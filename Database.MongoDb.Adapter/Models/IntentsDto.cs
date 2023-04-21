using Chatbot.Domain.Models;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.MongoDb.Adapter.Models
{
    [BsonIgnoreExtraElements]
    public class IntentsDto
    {
        [BsonElement("tenantId")]
        public int TenantId { get; set; }

        [BsonElement("tenantName")]
        public string TenantName { get; set; } = string.Empty;

        [BsonElement("intents")]
        public string Intents { get; set; }
    }
}
