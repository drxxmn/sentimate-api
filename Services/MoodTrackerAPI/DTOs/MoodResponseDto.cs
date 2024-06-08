namespace MoodTrackingService.DTOs;

public class MoodEntryResponseDto
{
    public string Id { get; set; }
    public string Mood { get; set; }
    public DateTime Timestamp { get; set; }
    public string UserId { get; set; }
}
