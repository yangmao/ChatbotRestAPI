using Chatbot.Domain.Models;
using Database.MongoDb.Adapter.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Database.MongoDb.Adapter
{
    public class IntentsContext: IIntentContext
    {
        private readonly IMongoCollection<IntentsCollections> _intentsCollection;

        public IntentsContext(IChatbotMongoDdatabaseSettings mongoDdatabaseSettings,IMongoClient mongoClient) 
        {
            var database = mongoClient.GetDatabase(mongoDdatabaseSettings.DatabaseName);
            _intentsCollection =  database.GetCollection<IntentsCollections>(mongoDdatabaseSettings.CollectionName);
        }

        public async Task CreateAsync(IntentsCollections intents)
        {
            await _intentsCollection.InsertOneAsync(intents);
        }

        public async Task<IntentsCollections> GetAsync()
        {
            var cusor = await _intentsCollection.FindAsync(x=>x.Intents != null);
            return  await cusor.FirstAsync();
        }

    }
}
