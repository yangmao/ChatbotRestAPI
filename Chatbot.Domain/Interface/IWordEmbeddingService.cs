using Chatbot.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chatbot.Domain.Interface
{
    public interface IWordEmbeddingService
    {
        Task<IEnumerable<Intent>> GetIntents();
        Task<string[]> GetVacabulary();

        Task<string[]> GetLables();

        Task<string> GetTraining();
    }
}