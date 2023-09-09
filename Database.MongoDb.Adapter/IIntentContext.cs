using Database.MongoDb.Adapter.Models;
using MongoDB.Bson;

namespace Database.MongoDb.Adapter
{
    public interface IIntentContext
    {
        public Task UpsertAsync(IntentCollection intents);
        public Task InsertManyAsync(IEnumerable<IntentCollection> intents);
        public Task<List<IntentCollection>> GetIntentsAsync();
    }
}
