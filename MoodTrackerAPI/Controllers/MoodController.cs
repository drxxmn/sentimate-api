using Microsoft.AspNetCore.Mvc;
using MoodTrackingService.Data;
using MoodTrackingService.Models;
using MongoDB.Driver;  // Ensure this is included
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoodTrackingService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoodController : ControllerBase
    {
        private readonly MongoDbContext _context;

        public MoodController(MongoDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MoodEntry>>> Get()
        {
            try
            {
                var entries = await _context.MoodEntries.Find(_ => true).ToListAsync();
                return Ok(entries);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<MoodEntry>> Post([FromBody] MoodEntry entry)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                entry.Timestamp = DateTime.UtcNow;
                await _context.MoodEntries.InsertOneAsync(entry);
                return CreatedAtAction(nameof(Get), new { id = entry.Id }, entry);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}