using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Database.MongoDb.Adapter.Models
{
    public class IntentCollection
    {
       
<<<<<<< HEAD
        public ObjectId Id { get; set; }
        
=======
        public Guid Id { get; set; }

        [BsonElement("userId")]
        public string? UserId { get; set; }

>>>>>>> origin/Test
        [BsonElement("tag")]
        public string? Tag { get; set; }

        [BsonElement("pattern")]
        public List<string>? Pattern { get; set; }

        [BsonElement("response")]
        public List<string>? Response { get; set; }
    }
}
