using Chatbot.Domain.Models;
using Chatbot.Domain.Ports;
using MongoDB.Bson.IO;
using Newtonsoft.Json;

namespace Database.MongoDb.Adapter
{
    public class IntentRepository : IIntentRepository
    {
        private readonly IIntentContext _intentContext;

        public IntentRepository(IIntentContext intentContext) 
        { 
            _intentContext = intentContext;
        }
        public async Task<List<Intent>> GetIntents()
        {
            var intentDto =  await _intentContext.GetAsync();
            var intents = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Intent>>(intentDto.Intents);
            return intents;
        }
    }
}
