using MongoDB.Bson.Serialization.Attributes;

namespace AzureMongoDbOnion03.Infrastructure.Dto.Model.Base
{
    public class BaseModel
    {
        [BsonId]
        public string Id { get; set; }
    }
}
