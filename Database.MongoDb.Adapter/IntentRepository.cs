using Chatbot.Domain.Models;
using Chatbot.Domain.Ports;

namespace Database.MongoDb.Adapter
{
    public class IntentRepository : IIntentRepository
    {
        public Task<List<Intent>> GetIntents()
        {
            throw new NotImplementedException();
        }
    }
}
