using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoodTrackingService.Data;
using MoodTrackingService.Models;
using MoodTrackingService.DTOs;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        [Authorize]
        public async Task<ActionResult<IEnumerable<MoodEntryResponseDto>>> Get()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var entries = await _context.MoodEntries.Find(entry => entry.UserId == userId).ToListAsync();
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
        [Authorize]
        public async Task<IActionResult> Post([FromBody] MoodEntryCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var entry = new MoodEntry
            {
                Mood = dto.Mood,
                UserId = userId,
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

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var entry = await _context.MoodEntries.Find(entry => entry.Id == id && entry.UserId == userId).FirstOrDefaultAsync();
            if (entry == null)
            {
                return NotFound();
            }

            await _context.MoodEntries.DeleteOneAsync(entry => entry.Id == id && entry.UserId == userId);
            return NoContent();
        }
    }

}
