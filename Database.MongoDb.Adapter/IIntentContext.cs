using Database.MongoDb.Adapter.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Database.MongoDb.Adapter
{
    public interface IIntentContext
    {
<<<<<<< HEAD
        public Task<List<IntentCollection>> GetAllAsync();
        public Task InsertManyAsync(IEnumerable<IntentCollection> intents);
        public Task UpsertOneAsync(IntentCollection intents);
        public Task<DeleteResult> DeleteOneAsync(string tag);
=======
        public Task<List<IntentCollection>> GetAllAsync(string userId);
        public Task InsertManyAsync(string userId,IEnumerable<IntentCollection> intents);
        public Task UpsertOneAsync(IntentCollection intents);
        public Task<DeleteResult> DeleteOneAsync(string userId, string tag);
>>>>>>> origin/Test
    }
}
