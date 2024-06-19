using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MoodTrackingService.Models
{
    public class MoodEntry
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        public int Mood { get; set; }

        public string UserId { get; set; } = string.Empty;

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}