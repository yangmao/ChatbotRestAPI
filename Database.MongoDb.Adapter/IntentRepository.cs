using Chatbot.Domain.Models;
using Chatbot.Domain.Ports;
using Database.MongoDb.Adapter.Models;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace Database.MongoDb.Adapter
{
    public class IntentRepository : IIntentRepository
    {
        private readonly IIntentContext? _intentContext;

        public IntentRepository(IIntentContext? intentContext) 
        { 
            _intentContext = intentContext;
        }

        public async Task UpsertAsync(string json)
        {
            var intents = JsonConvert.DeserializeObject<Dictionary<string, List<IntentDto>>>(json);
            var intentsCollection = new IntentsCollections()
            {
                Intents = JsonConvert.SerializeObject(intents.Values.FirstOrDefault())
            };
            await _intentContext!.UpsertAsync(intentsCollection);
        }
        public async Task AddIntent(string json)
        {
            var document = BsonSerializer.Deserialize<BsonDocument>(json);
            await _intentContext!.CreateAsync(document);
        }

        public async Task<IEnumerable<Intent>> GetIntents()
        {
            var intentCollection = await _intentContext.GetAsync();
            var intentDtos = JsonConvert.DeserializeObject<List<IntentDto>>(intentCollection.Intents);
            return intentDtos.Select(x => new Intent
            {
                Tag = x.Tag,
                Pattern = JsonConvert.SerializeObject(x.Pattern),
                Response = JsonConvert.SerializeObject(x.Response)
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
