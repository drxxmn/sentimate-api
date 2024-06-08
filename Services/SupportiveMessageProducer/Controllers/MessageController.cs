using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System.Text;

namespace SupportiveMessageProducer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post([FromBody] string message)
        {
            var factory = new ConnectionFactory() { HostName = "rabbitmq" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queue: "supportive_messages", durable: false, exclusive: false, autoDelete: false, arguments: null);

            var body = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish(exchange: "", routingKey: "supportive_messages", basicProperties: null, body: body);

            return Ok("Message sent to RabbitMQ");
        }
    }
}