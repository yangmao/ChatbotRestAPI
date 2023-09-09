using Chatbot.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chatbot.Domain.Ports
{
    public interface IIntentRepository
    {
        Task UpsertAsync(string json);
        Task<IEnumerable<Intent>> GetIntents();

        Task AddIntents(string json);

        Task RemoveIntent(string tag);

        Task UpdateIntent(Intent intent);
    }
}
