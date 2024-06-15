using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using SupportiveMessageService.Data;
using SupportiveMessageService.Models;

namespace SupportiveMessageService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SupportiveMessageController : ControllerBase
    {
        private readonly MongoDbContext _context;

        public SupportiveMessageController(MongoDbContext context)
        {
            _context = context;
        }

        [HttpGet("random")]
        public async Task<IActionResult> GetRandomMessage()
        {
            var collection = _context.SupportiveMessages;
            var count = await collection.CountDocumentsAsync(FilterDefinition<SupportiveMessage>.Empty);
            if (count == 0)
            {
                return NotFound("No supportive messages found.");
            }
            var randomIndex = new Random().Next(0, (int)count);
            var randomMessage = await collection.Find(FilterDefinition<SupportiveMessage>.Empty).Skip(randomIndex).Limit(1).FirstOrDefaultAsync();
            return Ok(randomMessage);
        }
    }
}