using Microsoft.AspNetCore.Mvc;
using MoodTrackingService.Data;
using MoodTrackingService.Models;
using MoodTrackingService.DTOs;
using MongoDB.Driver;
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
        public async Task<ActionResult<IEnumerable<MoodEntryResponseDto>>> Get()
        {
            var entries = await _context.MoodEntries.Find(_ => true).ToListAsync();
            var responseDtos = entries.ConvertAll(entry => new MoodEntryResponseDto
            {
                Id = entry.Id,
                Mood = entry.Mood,
                Timestamp = entry.Timestamp,
                UserId = entry.UserId
            });
            return Ok(responseDtos);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MoodEntryCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entry = new MoodEntry
            {
                Mood = dto.Mood,
                UserId = dto.UserId,
                Timestamp = DateTime.UtcNow
            };

            await _context.MoodEntries.InsertOneAsync(entry);
            var responseDto = new MoodEntryResponseDto
            {
                Id = entry.Id,
                Mood = entry.Mood,
                Timestamp = entry.Timestamp,
                UserId = entry.UserId
            };
            return CreatedAtAction(nameof(Get), new { id = entry.Id }, responseDto);
        }

        // New Delete method
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _context.MoodEntries.DeleteOneAsync(entry => entry.Id == id);
            if (result.DeletedCount == 0)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
