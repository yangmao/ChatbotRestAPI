using Chatbot.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chatbot.Domain.Ports
{
    public interface IIntentRepository
    {
        Task UpsertOne(string userId,string json);
        Task UpsertIntent(string userId, string json);
        Task<IEnumerable<Intent>> GetIntents(string userId);
        Task RemoveIntent(string userId, string tag);

    }
}
