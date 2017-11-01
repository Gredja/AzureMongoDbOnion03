using AzureMongoDbOnion03.Infrastructure.Data.Model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace AzureMongoDbOnion03.Infrastructure.Data.Helpers
{
    public class MongoDbContex
    {
        private readonly IMongoDatabase _database;

        public MongoDbContex(IOptions<Settings> settings)
        {
            _database = new MongoClient(settings.Value.ConnectionString).GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<Dto.Debtor> Debtors => _database.GetCollection<Dto.Debtor>("Debtor");

        public IMongoCollection<Dto.Credit> Credits => _database.GetCollection<Dto.Credit>("Credit");
    }
}
