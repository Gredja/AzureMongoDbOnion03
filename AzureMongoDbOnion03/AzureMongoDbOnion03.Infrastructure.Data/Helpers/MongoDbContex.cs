using AzureMongoDbOnion03.Infrastructure.Data.Model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace AzureMongoDbOnion03.Infrastructure.Data.Helpers
{
    public class MongoDbContex<T>
    {
        private readonly IMongoDatabase _database;

        public MongoDbContex(IOptions<Settings> settings)
        {
            _database = new MongoClient(settings.Value.ConnectionString).GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<T> Collection => _database.GetCollection<T>(typeof(T).Name);
    }
}
