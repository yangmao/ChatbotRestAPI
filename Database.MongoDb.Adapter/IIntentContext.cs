using Database.MongoDb.Adapter.Models;
using MongoDB.Bson;

namespace Database.MongoDb.Adapter
{
    public interface IIntentContext
    {
        public Task CreateAsync(BsonDocument document);
        public Task UpsertAsync(IntentsCollections intents);
        public Task InsertManyAsync(IEnumerable<IntentCollection> intents);
        public Task<List<IntentCollection>> GetIntentsAsync();
        public Task<IntentsCollections> GetAsync();
    }
}
