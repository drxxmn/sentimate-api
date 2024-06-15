using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using SupportiveMessageService.Data;
using SupportiveMessageService.Models;
using System;
using System.Linq;

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

        [HttpPost]
        public IActionResult CreateSupportiveMessage([FromBody] SupportiveMessage message)
        {
            _context.SupportiveMessages.InsertOne(message);
            return Ok("Message stored in the database.");
        }

        [HttpGet("random")]
        public IActionResult GetRandomMessage()
        {
            var count = _context.SupportiveMessages.CountDocuments(FilterDefinition<SupportiveMessage>.Empty);
            if (count == 0)
            {
                return NotFound("No messages found.");
            }

            var random = new Random();
            var skip = random.Next((int)count);
            var message = _context.SupportiveMessages.Find(FilterDefinition<SupportiveMessage>.Empty).Skip(skip).FirstOrDefault();
            return Ok(message);
        }
    }
}