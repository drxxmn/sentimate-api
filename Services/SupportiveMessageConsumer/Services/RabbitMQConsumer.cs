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
using System.Threading;
using System.Threading.Tasks;

namespace SupportiveMessageConsumer.Services
{
    public class RabbitMQConsumer : BackgroundService
    {
        private readonly ILogger<RabbitMQConsumer> _logger;
        private readonly MongoDbContext _context;
        private readonly string _hostName;
        private readonly string _queueName;
        private readonly string _username;
        private readonly string _password;

        public RabbitMQConsumer(IOptions<RabbitMQSettings> rabbitMqOptions, ILogger<RabbitMQConsumer> logger, MongoDbContext context)
        {
            _logger = logger;
            _context = context;
            var rabbitMqSettings = rabbitMqOptions.Value;
            _hostName = rabbitMqSettings.Host;
            _queueName = rabbitMqSettings.QueueName;
            _username = rabbitMqSettings.Username;
            _password = rabbitMqSettings.Password;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("RabbitMQ Consumer started");
            var factory = new ConnectionFactory()
            {
                HostName = _hostName,
                UserName = _username,
                Password = _password
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                _logger.LogInformation($"Message received: {message}");

                var supportiveMessage = JsonSerializer.Deserialize<SupportiveMessage>(message);
                if (supportiveMessage != null)
                {
                    _logger.LogInformation("Deserialized message successfully");
                    _logger.LogInformation($"Saving message to database: {JsonSerializer.Serialize(supportiveMessage)}");
                    await _context.SupportiveMessages.InsertOneAsync(supportiveMessage);
                    _logger.LogInformation("Message saved to database");
                }
            };

            channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);

            return Task.CompletedTask;
        }
    }
}
