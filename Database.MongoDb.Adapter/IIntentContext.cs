using Database.MongoDb.Adapter.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Database.MongoDb.Adapter
{
    public interface IIntentContext
    {
        public Task UpsertOneAsync(IntentCollection intents);
        public Task<List<IntentCollection>> GetAllAsync(string userId);
        public Task InsertManyAsync(string userId,IEnumerable<IntentCollection> intents);
        public Task<DeleteResult> DeleteOneAsync(string userId, string tag);
        public Task<DeleteResult> DeleteManyAsync(string userId);
    }
}
