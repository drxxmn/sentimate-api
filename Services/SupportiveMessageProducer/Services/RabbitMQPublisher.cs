using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

public class RabbitMQPublisher
{
    private readonly string _hostName;
    private readonly string _queueName;
    private readonly string _username;
    private readonly string _password;

    public RabbitMQPublisher(IConfiguration configuration)
    {
        _hostName = configuration["RabbitMQ:Host"];
        _queueName = configuration["RabbitMQ:QueueName"];
        _username = configuration["RabbitMQ:Username"];
        _password = configuration["RabbitMQ:Password"];
    }

    public void PublishMessage(object message)
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

        var messageBody = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
        channel.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: null, body: messageBody);
    }
}