using AzureMongoDbOnion03.Helpers;
using AzureMongoDbOnion03.Infrastructure.Dto.Model.Base;
using MongoDB.Bson.Serialization.Attributes;

namespace AzureMongoDbOnion03.Infrastructure.Dto.Model
{
    public class User : BaseModel
    {
        [BsonRequired]
        public string ForeignId { get; set; }

        [BsonRequired]
        public string Email { get; set; }

        [BsonRequired]
        public string Password { get; set; }

        public Role Role { get; set; }
    }
}
