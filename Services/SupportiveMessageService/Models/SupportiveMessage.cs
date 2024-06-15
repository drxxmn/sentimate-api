using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace SupportiveMessageService.Models
{
    public class SupportiveMessage
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }  // Mark as nullable if it will be assigned later

        [BsonElement("Content")]
        public string Content { get; set; } = string.Empty;  // Provide a default value

        [BsonElement("Timestamp")]
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}