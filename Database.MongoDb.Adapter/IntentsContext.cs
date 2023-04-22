using Chatbot.Domain.Models;
using Database.MongoDb.Adapter.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Database.MongoDb.Adapter
{
    public class IntentsContext: IIntentContext
    {
        private readonly IMongoCollection<IntentsDto> _intentsDto;

        public IntentsContext(IChatbotMongoDdatabaseSettings mongoDdatabaseSettings,IMongoClient mongoClient) 
        {
            var database = mongoClient.GetDatabase(mongoDdatabaseSettings.DatabaseName);
            _intentsDto =  database.GetCollection<IntentsDto>(mongoDdatabaseSettings.CollectionName);
        }

        

        public async Task CreateTenant(IntentsDto intents)
        {
            await _intentsDto.InsertOneAsync(intents);
        }

        public async Task<IntentsDto> GetAsync()
        {
            var cusor = await _intentsDto.FindAsync(x=>x.TenantId == 1);
            return  await cusor.FirstAsync();
        }

       
    }
}
