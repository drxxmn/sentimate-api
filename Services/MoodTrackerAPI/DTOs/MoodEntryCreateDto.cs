namespace MoodTrackingService.DTOs
{
    public class MoodEntryCreateDto
    {
        public int Mood { get; set; }
        public string UserId { get; set; } = string.Empty;
    }
}