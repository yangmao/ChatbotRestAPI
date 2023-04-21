using Chatbot.Domain.Models;
using Database.MongoDb.Adapter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.MongoDb.Adapter
{
    public interface IIntentContext
    {
        public void CreateTenant(IntentsDto intents);
        public string CreateIntent(Intent intents);
        public bool Update(int id, IntentsDto intentsDto);
        public bool DeleteTenent(int tenantId);
        public IntentsDto Get(int tenantId);
    }
}
