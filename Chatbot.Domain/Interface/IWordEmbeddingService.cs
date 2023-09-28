using Chatbot.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chatbot.Domain.Interface
{
    public interface IWordEmbeddingService
    {
        Task<IEnumerable<Intent>> GetIntents(string userId);
        Task<string[]> GetVacabulary(string userId);

        Task<string[]> GetLables(string userId);

        Task<string> GetTraining(string userId);
    }
}