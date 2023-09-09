using Chatbot.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chatbot.Domain.Ports
{
    public interface IIntentRepository
    {
        Task AddIntents(string json);
        Task UpsertIntent(string json);
        Task<IEnumerable<Intent>> GetIntents();
        Task RemoveIntent(string tag);

    }
}
