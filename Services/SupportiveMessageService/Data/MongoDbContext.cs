using MongoDB.Driver;
using SupportiveMessageService.Models;

namespace SupportiveMessageService.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetConnectionString("MongoDb"));
            _database = client.GetDatabase("supportivemessagesdb");
        }

        public IMongoCollection<SupportiveMessage> SupportiveMessages => _database.GetCollection<SupportiveMessage>("SupportiveMessages");
    }
}