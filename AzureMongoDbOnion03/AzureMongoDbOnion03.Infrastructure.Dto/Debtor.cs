using MongoDB.Bson.Serialization.Attributes;

namespace AzureMongoDbOnion03.Infrastructure.Dto
{
    public class Debtor
    {
        [BsonId]
        public string Id { get; set; }

        [BsonRequired]
        public string Name { get; set; }
    }
}
