using Chatbot.Domain.Models;
using Database.MongoDb.Adapter.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using static MongoDB.Driver.WriteConcern;

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

        public async Task UpdateAsync(string tag, string pattern)
        {
            //var id = ObjectId.Parse("5b9f91b9ecde570d2cf645e5"); // your document Id
            //var builder = Builders<IntentsCollections>.Filter;
            //var filter = builder.Eq(x => x.Id, id);
            //var update = Builders<IntentsCollections>.Update
            //    .AddToSet(x => x.Intents, new Intent
            //    {
                   

            //    }) ;
            //var updateResult = await _intentsCollection.UpdateOneAsync(filter, update);
        }

        public async Task<IntentsCollections> GetAsync()
        {
            var cusor = await _intentsCollection.FindAsync(x=>x.Intents != null);
            return  await cusor.FirstAsync();
        }

    }
}
