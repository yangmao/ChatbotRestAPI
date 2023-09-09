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
        private readonly IMongoCollection<IntentCollection> _intentCollection;

        public IntentsContext(IChatbotMongoDdatabaseSettings mongoDdatabaseSettings,IMongoClient mongoClient) 
        {
            var database = mongoClient.GetDatabase(mongoDdatabaseSettings.DatabaseName);
            _intentCollection = database.GetCollection<IntentCollection>(mongoDdatabaseSettings.CollectionName);
        }

        public async Task<List<IntentCollection>> GetAllAsync()
        {
            return await _intentCollection.Find(_ => true).ToListAsync();
        }

        public async Task InsertManyAsync(IEnumerable<IntentCollection> intents)
        {
            await _intentCollection.InsertManyAsync(intents);
        }

        public async Task UpsertOneAsync(IntentCollection intent)
        {
             var result = await _intentCollection.ReplaceOneAsync(
                            filter: new BsonDocument("tag", intent.Tag),
                            options: new ReplaceOptions { IsUpsert = true },
                            replacement: intent);
        }

        public async Task<DeleteResult> DeleteOneAsync(string tag)
        {
            var filter = Builders<IntentCollection>.Filter
                .Eq(r => r.Tag, tag);
            return await _intentCollection.DeleteOneAsync(filter);
        }
        
    }
}
