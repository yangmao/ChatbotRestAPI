using Chatbot.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chatbot.Domain.Ports
{
    public interface IIntentRepository
    {
        Task CreateAsync(string json);
        Task<IEnumerable<Intent>> GetIntents();

        Task AddIntent(Intent intent);

        Task RemoveIntent(string tag);

        Task UpdateIntent(Intent intent);
    }
}
