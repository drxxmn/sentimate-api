using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SupportiveMessageProducer.Models;
using SupportiveMessageProducer.Services;

namespace SupportiveMessageProducer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SupportiveMessageController : ControllerBase
    {
        private readonly RabbitMQPublisher _rabbitMQPublisher;
        private readonly ILogger<SupportiveMessageController> _logger;

        public SupportiveMessageController(RabbitMQPublisher rabbitMQPublisher, ILogger<SupportiveMessageController> logger)
        {
            _rabbitMQPublisher = rabbitMQPublisher;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult SendSupportiveMessage([FromBody] SupportiveMessage message)
        {
            _logger.LogInformation("Endpoint hit: /SupportiveMessage");
            _logger.LogInformation($"Received supportive message: {message.Content}");
            _rabbitMQPublisher.PublishMessage(message);
            return Ok("Message sent to queue");
        }
        
        [HttpGet("test")]
        public IActionResult Test()
        {
            _logger.LogInformation("Test endpoint hit");
            return Ok("Test endpoint working");
        }
    }
}


        
    