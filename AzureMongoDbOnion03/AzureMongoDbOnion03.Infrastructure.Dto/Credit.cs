using System;
using MongoDB.Bson.Serialization.Attributes;

namespace AzureMongoDbOnion03.Infrastructure.Dto
{
    public class Credit
    {
        [BsonId]
        public string Id { get; set; }

        [BsonRequired]
        public string ForeignId { get; set; }

        [BsonRequired]
        public string Currency { get; set; }

        [BsonRequired]
        public int Amount { get; set; }

        public DateTime ExpirationDate { get; set; }

        [BsonRequired]
        public bool Active { get; set; } = true;

        public string Comment { get; set; }
    }
}
