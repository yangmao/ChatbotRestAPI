using Chatbot.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chatbot.Domain.Ports
{
    public interface IIntentRepository
    {
<<<<<<< HEAD
        Task AddIntents(string json);
        Task UpsertIntent(string json);
        Task<IEnumerable<Intent>> GetIntents();
        Task RemoveIntent(string tag);
=======
        Task AddIntents(string userId,string json);
        Task UpsertIntent(string userId, string json);
        Task<IEnumerable<Intent>> GetIntents(string userId);
        Task RemoveIntent(string userId, string tag);
>>>>>>> origin/Test

    }
}
