using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RinhaDeBackEnd2023.Models
{
    public class BaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }
    }
}