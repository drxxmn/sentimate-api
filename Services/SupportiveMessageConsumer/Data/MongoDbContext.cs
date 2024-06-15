using MongoDB.Driver;
using SupportiveMessageConsumer.Models;
using Microsoft.Extensions.Options;

namespace SupportiveMessageConsumer.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);
        }

        public IMongoCollection<SupportiveMessage> SupportiveMessages
        {
            get { return _database.GetCollection<SupportiveMessage>("SupportiveMessages"); }
        }
    }
}