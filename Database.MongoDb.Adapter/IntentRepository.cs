using Chatbot.Domain.Models;
using Chatbot.Domain.Ports;
using Database.MongoDb.Adapter.Models;
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

        public async Task AddIntents(string json)
        {
            var intents = JsonConvert.DeserializeObject<Dictionary<string, List<Intent>>>(json);
            var intentsCollection = intents.Values?.FirstOrDefault()?.Select(x => new IntentCollection
            {
                Tag = x.Tag,
                Pattern = x.Pattern,
                Response = x.Response
            });
            await _intentContext!.InsertManyAsync(intentsCollection!);
        }

        public async Task<IEnumerable<Intent>> GetIntents()
        {
            var intentCollection = await _intentContext!.GetIntentsAsync();
            return intentCollection.Select(x => new Intent
            {
                Tag = x.Tag,
                Pattern = x.Pattern,
                Response = x.Response
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

        public async Task UpsertAsync(string json)
        {
            var intent = JsonConvert.DeserializeObject<Intent>(json);
            var intentCollection = new IntentCollection()
            {
                Tag = intent.Tag,
                Pattern = intent.Pattern,
                Response = intent.Response
            };
            await _intentContext!.UpsertAsync(intentCollection);
        }
    }
}
