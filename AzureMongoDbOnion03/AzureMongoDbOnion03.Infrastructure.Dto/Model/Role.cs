
using AzureMongoDbOnion03.Infrastructure.Dto.Model.Base;
using MongoDB.Bson.Serialization.Attributes;

namespace AzureMongoDbOnion03.Infrastructure.Dto.Model
{
    public class Role : BaseModel
    {
        [BsonRequired]
        public string Name { get; set; }
    }
}
