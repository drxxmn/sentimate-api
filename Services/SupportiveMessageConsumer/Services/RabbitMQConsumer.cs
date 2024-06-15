using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SupportiveMessageConsumer.Data;
using SupportiveMessageConsumer.Models;
using System.Text;
using System.Text.Json;

namespace SupportiveMessageConsumer.Services
{
    public class RabbitMQConsumer : BackgroundService
    {
        private readonly ILogger<RabbitMQConsumer> _logger;
        private readonly MongoDbContext _mongoDbContext;
        private readonly string _hostName;
        private readonly string _queueName;
        private readonly string _username;
        private readonly string _password;
        private IConnection? _connection;
        private IModel? _channel;

        public RabbitMQConsumer(IOptions<RabbitMQSettings> rabbitMqSettings, MongoDbContext mongoDbContext, ILogger<RabbitMQConsumer> logger)
        {
            _logger = logger;
            _mongoDbContext = mongoDbContext;
            _hostName = rabbitMqSettings.Value.Host;
            _queueName = rabbitMqSettings.Value.QueueName;
            _username = rabbitMqSettings.Value.Username;
            _password = rabbitMqSettings.Value.Password;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var factory = new ConnectionFactory()
            {
                HostName = _hostName,
                UserName = _username,
                Password = _password,
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                _logger.LogInformation($"Message received: {message}");
                var supportiveMessage = JsonSerializer.Deserialize<SupportiveMessage>(message);

                if (supportiveMessage != null)
                {
                    _logger.LogInformation("Deserialized message successfully");
                    _logger.LogInformation($"Saving message to database: {JsonSerializer.Serialize(supportiveMessage)}");
                    _mongoDbContext.SupportiveMessages.InsertOne(supportiveMessage);
                    _logger.LogInformation("Message saved to database");
                }
                else
                {
                    _logger.LogError("Failed to deserialize message");
                }
            };

            _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);

            _logger.LogInformation("RabbitMQ Consumer started");
            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            _channel?.Close();
            _connection?.Close();
            base.Dispose();
        }
    }
}
