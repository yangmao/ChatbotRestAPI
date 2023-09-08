using Chatbot.Domain.Models;
using Database.MongoDb.Adapter.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.MongoDb.Adapter
{
    public interface IIntentContext
    {
        public Task CreateAsync(BsonDocument document);
        public Task UpsertAsync(IntentsCollections intents);
        public Task<IntentsCollections> GetAsync();
    }
}
