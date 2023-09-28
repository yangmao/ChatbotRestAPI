using Amazon.SecurityToken.Model;
using Database.MongoDb.Adapter.Models;
using MongoDB.Bson;
using MongoDB.Driver;

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
            var filter = Builders<IntentCollection>.Filter.Empty;
            await _intentCollection.DeleteManyAsync(filter);
            await _intentCollection.InsertManyAsync(intents);
        }

        public async Task UpsertOneAsync(IntentCollection intent)
        {
            var filter = Builders<IntentCollection>.Filter
               .Eq(r => r.Tag, intent.Tag) & Builders<IntentCollection>.Filter
               .Eq(r => r.UserId, intent.UserId);

            var existedIntent = await _intentCollection.FindAsync(filter).Result.ToListAsync();

            if (existedIntent.Count != 0)
                intent.Id = existedIntent.Select(x=>x.Id).First();
             await _intentCollection.ReplaceOneAsync(
                        filter: new BsonDocument("tag", intent.Tag),
                        options: new ReplaceOptions { IsUpsert = true },
                        replacement: intent);
        }

        public async Task<DeleteResult> DeleteOneAsync(string userId,string tag)
        {
            var filter = Builders<IntentCollection>.Filter
                .Eq(r => r.Tag, tag) & Builders<IntentCollection>.Filter
                .Eq(r => r.UserId, userId);

            return await _intentCollection.DeleteOneAsync(filter);
        }
        
    }
}
