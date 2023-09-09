using Chatbot.Domain.Models;
using Database.MongoDb.Adapter.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Drawing;
using static MongoDB.Driver.WriteConcern;

namespace Database.MongoDb.Adapter
{
    public class IntentsContext: IIntentContext
    {
        private readonly IMongoCollection<IntentsCollections> _intentsCollection;
        private readonly IMongoCollection<IntentCollection> _intentCollection;

        public IntentsContext(IChatbotMongoDdatabaseSettings mongoDdatabaseSettings,IMongoClient mongoClient) 
        {
            var database = mongoClient.GetDatabase(mongoDdatabaseSettings.DatabaseName);
            _intentsCollection =  database.GetCollection<IntentsCollections>(mongoDdatabaseSettings.CollectionName);
            _intentCollection = database.GetCollection<IntentCollection>(mongoDdatabaseSettings.CollectionName);
        }

        public async Task CreateAsync(BsonDocument document)
        {
            //await _intentsCollection.InsertOneAsync(document);
        }

        public async Task UpsertAsync(IntentsCollections intents)
        {
             var result = await _intentsCollection.ReplaceOneAsync(
                            filter: new BsonDocument("_id", intents.Id),
                            options: new ReplaceOptions { IsUpsert = true },
                            replacement: intents);
        }

        public async Task InsertManyAsync(IEnumerable<IntentCollection> intents)
        {
            await _intentCollection.InsertManyAsync(intents);
        }
        public async Task<IntentsCollections> GetAsync()
        {
            var cusor = await _intentsCollection.FindAsync(x=>x.Intents != null);
            return  await cusor.FirstAsync();
        }

        public async Task<List<IntentCollection>> GetIntentsAsync()
        {
            return await _intentCollection.Find(_ => true).ToListAsync();
        }
    }
}
