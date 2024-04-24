using MongoDB.Driver;
using MoodTrackingService.Models;

namespace MoodTrackingService.Data
{
    public interface IMongoDbContext
    {
        IMongoCollection<MoodEntry> MoodEntries { get; }
    }
}