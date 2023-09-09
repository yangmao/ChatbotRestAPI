﻿using Chatbot.Domain.Models;
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

        public async Task UpsertAsync(string json)
        {
            var intents = JsonConvert.DeserializeObject<Dictionary<string, List<Intent>>>(json);
            var intentsCollection = new IntentsCollections()
            {
                Intents = JsonConvert.SerializeObject(intents.Values.FirstOrDefault())
            };
            await _intentContext!.UpsertAsync(intentsCollection);
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
            var intentCollection = await _intentContext.GetIntentsAsync();
            return intentCollection.Select(x => new Intent
            {
                Tag = x.Tag,
                Pattern = x.Pattern,
                Response = x.Response
            });
        }

        //public async Task<IEnumerable<Intent>> GetIntents()
        //{
        //    var intentCollection = await _intentContext.GetAsync();
        //    var intentDtos = JsonConvert.DeserializeObject<List<IntentDto>>(intentCollection.Intents);
        //    return intentDtos.Select(x => new Intent
        //    {
        //        Tag = x.Tag,
        //        Pattern = JsonConvert.SerializeObject(x.Pattern),
        //        Response = JsonConvert.SerializeObject(x.Response)
        //    });
        //}
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
