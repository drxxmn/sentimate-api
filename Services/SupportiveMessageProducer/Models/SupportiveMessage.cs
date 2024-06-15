using System;

namespace SupportiveMessageProducer.Models
{
    public class SupportiveMessage
    {
        public string Content { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}