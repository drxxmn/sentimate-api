using MongoDB.Driver;
using MoodTrackingService.Models;
using Microsoft.Extensions.Configuration;

namespace MoodTrackingService.Data
{
    public class MongoDbContext : IMongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("MongoDB:ConnectionString"));
            _database = client.GetDatabase(configuration.GetValue<string>("MongoDB:DatabaseName"));
        }

        public IMongoCollection<MoodEntry> MoodEntries => _database.GetCollection<MoodEntry>("MoodEntries");
    }
}

