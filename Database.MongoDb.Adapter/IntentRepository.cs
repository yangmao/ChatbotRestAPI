using Chatbot.Domain.Models;
using Chatbot.Domain.Ports;
using Database.MongoDb.Adapter.Models;
using MongoDB.Bson.IO;
using Newtonsoft.Json;
using System.Security.Cryptography.X509Certificates;

namespace Database.MongoDb.Adapter
{
    public class IntentRepository : IIntentRepository
    {
        private readonly IIntentContext _intentContext;

        public IntentRepository(IIntentContext intentContext) 
        { 
            _intentContext = intentContext;
        }

        public Task AddIntent(Intent intent)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Intent>> GetIntents()
        {
            var intentCollection =  await _intentContext.GetAsync();
            var intentDtos = Newtonsoft.Json.JsonConvert.DeserializeObject<List<IntentDto>>(intentCollection.Intents);
            var intents = new List<Intent>();
            return intentDtos.Select(x => new Intent
            {
                Tag = x.Tag,
                Pattern = Newtonsoft.Json.JsonConvert.SerializeObject(x.Pattern),
                Response = Newtonsoft.Json.JsonConvert.SerializeObject(x.Response)
            });
        }

        public Task RemoveIntent(string tag)
        {
            throw new NotImplementedException();
        }

        public Task UpdateIntent(Intent intent)
        {
            throw new NotImplementedException();
        }
    }
}
