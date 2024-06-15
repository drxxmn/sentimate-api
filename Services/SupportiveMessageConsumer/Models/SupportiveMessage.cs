using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace SupportiveMessageConsumer.Models
{
    public class SupportiveMessage
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Content")]
        public string Content { get; set; }

        [BsonElement("Timestamp")]
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}