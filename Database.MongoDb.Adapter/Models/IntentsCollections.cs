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
    public class IntentsCollections
    {
        [BsonElement("intents")]
        public string Intents { get; set; }
    }
}
