using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using MoodTrackingService.Data;
using MoodTrackingService.Models;
using MoodTrackingService.DTOs;
using MongoDB.Driver;
using System;

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

        private string GetUserIdFromToken()
        {
            var authHeader = HttpContext.Request.Headers["Authorization"].ToString();
            if (authHeader.StartsWith("Bearer "))
            {
                var token = authHeader.Substring("Bearer ".Length).Trim();
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
                var userId = jsonToken?.Claims.First(claim => claim.Type == "sub")?.Value;
                return userId;
            }
            return null;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<MoodEntryResponseDto>>> Get()
        {
            var userId = GetUserIdFromToken();
            if (userId == null)
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

            var userId = GetUserIdFromToken();
            if (userId == null)
            {
                return Unauthorized();
            }

            var userTimezoneOffset = HttpContext.Request.Headers["Timezone-Offset"]; // Expecting the offset in minutes as a header

            if (!int.TryParse(userTimezoneOffset, out int offsetMinutes))
            {
                return BadRequest("Invalid timezone offset.");
            }

            var currentDateTimeUtc = DateTime.UtcNow;
            var userCurrentDateTime = currentDateTimeUtc.AddMinutes(offsetMinutes);
            var userCurrentDate = userCurrentDateTime.Date;

            var filter = Builders<MoodEntry>.Filter.Where(entry => entry.UserId == userId && entry.Timestamp >= userCurrentDate && entry.Timestamp < userCurrentDate.AddDays(1));
            var existingEntry = await _context.MoodEntries.Find(filter).FirstOrDefaultAsync();

            if (existingEntry != null)
            {
                var update = Builders<MoodEntry>.Update.Set(entry => entry.Mood, dto.Mood).Set(entry => entry.Timestamp, currentDateTimeUtc);
                await _context.MoodEntries.UpdateOneAsync(filter, update);
            }
            else
            {
                var entry = new MoodEntry
                {
                    Mood = dto.Mood,
                    UserId = userId,
                    Timestamp = currentDateTimeUtc
                };

                await _context.MoodEntries.InsertOneAsync(entry);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            var userId = GetUserIdFromToken();
            if (userId == null)
            {
                return Unauthorized();
            }

            var entry = await _context.MoodEntries.Find(entry => entry.Id == id && entry.UserId == userId).FirstOrDefaultAsync();
            if (entry == null)
            {
                return NotFound();
            }

            var result = await _context.MoodEntries.DeleteOneAsync(entry => entry.Id == id && entry.UserId == userId);
            if (result.DeletedCount == 0)
            {
                return NotFound();
            }

            return NoContent();
        }

        // New endpoint to delete all user-related data
        [HttpDelete]
        [Route("deleteAll")]
        [Authorize]
        public async Task<IActionResult> DeleteAllUserData()
        {
            var userId = GetUserIdFromToken();
            if (userId == null)
            {
                return Unauthorized();
            }

            var result = await _context.MoodEntries.DeleteManyAsync(entry => entry.UserId == userId);
            if (result.DeletedCount == 0)
            {
                return NotFound("No data found for the user.");
            }

            return NoContent();
        }
    }
}
