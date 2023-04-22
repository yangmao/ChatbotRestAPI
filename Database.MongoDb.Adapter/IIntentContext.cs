using Chatbot.Domain.Models;
using Database.MongoDb.Adapter.Models;
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
        public Task CreateTenant(IntentsDto intents);
        
        public Task<IntentsDto> GetAsync();
    }
}
