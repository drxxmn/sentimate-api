namespace MoodTrackingService.DTOs
{
    public class MoodEntryResponseDto
    {
        public string Id { get; set; } = string.Empty;
        public int Mood { get; set; }
        public string UserId { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
    }
}