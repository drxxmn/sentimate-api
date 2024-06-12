using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;

public class RabbitMQPublisher
{
    private readonly string _hostName;
    private readonly string _queueName;
    private readonly string _username;
    private readonly string _password;
    private readonly ILogger<RabbitMQPublisher> _logger;

    public RabbitMQPublisher(IConfiguration configuration, ILogger<RabbitMQPublisher> logger)
    {
        _hostName = configuration["RabbitMQ:Host"];
        _queueName = configuration["RabbitMQ:QueueName"];
        _username = configuration["RabbitMQ:Username"];
        _password = configuration["RabbitMQ:Password"];
        _logger = logger;
    }

    public void PublishMessage(object message)
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
            channel.QueueDeclare(queue: _queueName, durable: false, exclusive