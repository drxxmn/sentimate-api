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
    }
}
