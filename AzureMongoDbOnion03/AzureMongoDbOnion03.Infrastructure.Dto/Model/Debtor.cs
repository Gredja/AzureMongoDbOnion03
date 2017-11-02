using AzureMongoDbOnion03.Infrastructure.Dto.Model.Base;
using MongoDB.Bson.Serialization.Attributes;

namespace AzureMongoDbOnion03.Infrastructure.Dto.Model
{
    public class Debtor : BaseModel
    {
        [BsonRequired]
        public string Name { get; set; }
    }
}
