using Chatbot.Domain.Models;
using Chatbot.Domain.Ports;
using Database.MongoDb.Adapter.Models;
using MongoDB.Driver;
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
        public async Task<IEnumerable<Intent>> GetIntents(string userId)
        {
            var intentCollection = await _intentContext!.GetAllAsync(userId);
            return intentCollection.Select(x => new Intent
            {
                Tag = x.Tag,
                Pattern = x.Pattern,
                Response = x.Response
            });
        }

        public async Task AddIntents(string userId, string json)
        {
            var intents = JsonConvert.DeserializeObject<Dictionary<string, List<Intent>>>(json);
            var intentsCollection = intents.Values?.FirstOrDefault()?.Select(x => new IntentCollection
            {
                UserId = userId,
                Tag = x.Tag,
                Pattern = x.Pattern,
                Response = x.Response
            });
            await _intentContext!.InsertManyAsync(userId,intentsCollection!);
        }
        public async Task UpsertIntent(string json)
        {
            var intent = JsonConvert.DeserializeObject<Intent>(json);
            var intentCollection = new IntentCollection()
            {
                Tag = intent.Tag,
                Pattern = intent.Pattern,
                Response = intent.Response
            };
            await _intentContext!.UpsertOneAsync(intentCollection);
        }

        public async Task UpsertIntent(string userId, string json)
        {
            var intent = JsonConvert.DeserializeObject<Intent>(json);
            var intentCollection = new IntentCollection()
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Tag = intent.Tag,
                Pattern = intent.Pattern,
                Response = intent.Response
            };
            await _intentContext!.UpsertOneAsync(intentCollection);
        }
        public async Task RemoveIntent(string userId, string tag)
        {
            await _intentContext!.DeleteOneAsync(userId,tag);
        }
        public async Task RemoveAllIntents(string userId)
        {
            await _intentContext!.DeleteManyAsync(userId);
        }

    }
}
