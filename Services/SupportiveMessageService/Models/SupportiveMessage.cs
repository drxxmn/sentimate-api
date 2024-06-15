using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SupportiveMessageService.Models
{
    public class SupportiveMessage
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
    }
}