using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SupportiveMessageService.Models;

namespace SupportiveMessageService.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);
        }

        public IMongoCollection<SupportiveMessage> SupportiveMessages => _database.GetCollection<SupportiveMessage>("SupportiveMessages");
    }
}