using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using SupportiveMessageService.Models;
using SupportiveMessageService.Data;
using Microsoft.Extensions.Logging;

namespace SupportiveMessageService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SupportiveMessageController : ControllerBase
    {
        private readonly MongoDbContext _context;
        private readonly ILogger<SupportiveMessageController> _logger;

        public SupportiveMessageController(MongoDbContext context, ILogger<SupportiveMessageController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("random")]
        public ActionResult<SupportiveMessage> GetRandomMessage()
        {
            _logger.LogInformation("Received request for a random supportive message.");

            var messageCount = _context.SupportiveMessages.CountDocuments(FilterDefinition<SupportiveMessage>.Empty);
            if (messageCount == 0)
            {
                _logger.LogWarning("No supportive messages found in the database.");
                return NotFound("No supportive messages found.");
            }

            var random = new Random();
            var skip = random.Next(0, (int)messageCount);
            _logger.LogInformation($"Fetching message by skipping {skip} documents out of {messageCount} total messages.");

            var message = _context.SupportiveMessages.Find(FilterDefinition<SupportiveMessage>.Empty).Skip(skip).FirstOrDefault();
            if (message == null)
            {
                _logger.LogError("Failed to retrieve a random supportive message.");
                return NotFound("No supportive messages found.");
            }

            _logger.LogInformation("Successfully retrieved a random supportive message.");
            return Ok(message);
        }
    }
}