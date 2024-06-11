using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System.Text;

namespace SupportiveMessageProducer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SupportiveMessageController : ControllerBase
    {
        private readonly RabbitMQPublisher _rabbitMQPublisher;

        public SupportiveMessageController(RabbitMQPublisher rabbitMQPublisher)
        {
            _rabbitMQPublisher = rabbitMQPublisher;
        }

        [HttpPost]
        public IActionResult SendSupportiveMessage([FromBody] SupportiveMessage message)
        {
            _rabbitMQPublisher.PublishMessage(message);
            return Ok("Message sent to queue");
        }
    }

    public class SupportiveMessage
    {
        public string Content { get; set; }
        public string Sender { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
