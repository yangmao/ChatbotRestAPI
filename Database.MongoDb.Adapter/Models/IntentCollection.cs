﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Database.MongoDb.Adapter.Models
{
    public class IntentCollection
    {
        [BsonElement("_id")]
        public ObjectId? Id { get; set; }
        
        [BsonElement("tag")]
        public string? Tag { get; set; }

        [BsonElement("pattern")]
        public List<string>? Pattern { get; set; }

        [BsonElement("response")]
        public List<string>? Response { get; set; }
    }
}
