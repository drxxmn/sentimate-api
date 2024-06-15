using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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
        private readonly string _hostName;
        private readonly string _queueName;
        private readonly string _username;
        private readonly string _password;
        private readonly ILogger<RabbitMQConsumer> _logger;
        private readonly MongoDbContext _context;

        public RabbitMQConsumer(IConfiguration configuration, ILogger<RabbitMQConsumer> logger, MongoDbContext context)
        {
            _hostName = configuration["RabbitMQ:Host"];
            _queueName = configuration["RabbitMQ:QueueName"];
            _username = configuration["RabbitMQ:Username"];
            _password = configuration["RabbitMQ:Password"];
            _logger = logger;
            _context = context;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
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
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    _logger.LogInformation($"Message received: {message}");
                    
                    try
                    {
                        var supportiveMessage = JsonSerializer.Deserialize<SupportiveMessage>(message);
                        SaveMessageToDatabase(supportiveMessage);
                    }
                    catch (JsonException ex)
                    {
                        _logger.LogError($"Failed to deserialize message: {ex.Message}");
                    }
                };

                channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to start RabbitMQ consumer: {ex.Message}");
            }
        }

        private void SaveMessageToDatabase(SupportiveMessage message)
        {
            try
            {
                _context.SupportiveMessages.InsertOne(message);
                _logger.LogInformation("Message saved to database");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to save message to database: {ex.Message}");
            }
        }
    }
}
