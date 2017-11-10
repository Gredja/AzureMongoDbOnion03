﻿using AzureMongoDbOnion03.Infrastructure.Dto.Model.Base;
using MongoDB.Bson.Serialization.Attributes;

namespace AzureMongoDbOnion03.Infrastructure.Dto.Model
{
    public class Credit : BaseModel
    {
        [BsonRequired]
        public string ForeignId { get; set; }

        [BsonRequired]
        public string Currency { get; set; }

        [BsonRequired]
        public int Amount { get; set; }

        [BsonRequired]
        public bool Active { get; set; } = true;

        public string Comment { get; set; }
    }
}
