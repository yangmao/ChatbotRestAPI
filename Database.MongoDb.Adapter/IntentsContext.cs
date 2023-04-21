using Chatbot.Domain.Models;
using Database.MongoDb.Adapter.Models;
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

        public string CreateIntent(Intent intents)
        {
            throw new NotImplementedException();
        }

        public void CreateTenant(IntentsDto intents)
        {
            _intentsDto.InsertOne(intents);
        }

        public bool DeleteTenent(int tenantId)
        {
            return _intentsDto.DeleteOne(x => x.TenantId == tenantId).IsAcknowledged;
        }

        public IntentsDto Get(int tenantId)
        {
            return _intentsDto.Find(x => x.TenantId == tenantId).FirstOrDefault();
        }

        public bool Update(int id, IntentsDto intentsDto)
        {
            return _intentsDto.ReplaceOne(x => x.TenantId == id, intentsDto).IsAcknowledged;
        }
    }
}
