namespace MoodTrackingService.DTOs
{
    public class MoodEntryResponseDto
    {
        public string Id { get; set; }
        public int Mood { get; set; }
        public string UserId { get; set; }
        public DateTime Timestamp { get; set; }
    }
}