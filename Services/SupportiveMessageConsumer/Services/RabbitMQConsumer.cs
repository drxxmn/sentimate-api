using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SupportiveMessageConsumer.Data;
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
        private readonly IServiceScopeFactory _scopeFactory;

        public RabbitMQConsumer(IConfiguration configuration, IServiceScopeFactory scopeFactory)
        {
            _hostName = configuration["RabbitMQ:Host"] ?? throw new ArgumentNullException(nameof(configuration), "RabbitMQ host name is not configured");
            _queueName = configuration["RabbitMQ:QueueName"] ?? throw new ArgumentNullException(nameof(configuration), "RabbitMQ queue name is not configured");
            _scopeFactory = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory));
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var factory = new ConnectionFactory() { HostName = _hostName };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var supportiveMessage = JsonSerializer.Deserialize<SupportiveMessage>(message);

                if (supportiveMessage != null)
                {
                    using (var scope = _scopeFactory.CreateScope())
                    {
                        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                        dbContext.SupportiveMessages.Add(supportiveMessage);
                        dbContext.SaveChanges();
                    }
                }
            };

            channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);

            return Task.CompletedTask;
        }
    }
}
