using Chatbot.Domain.Interface;
using Chatbot.Domain.Models;
using Chatbot.Domain.Ports;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chatbot.Domain.concrete
{
    public class DataTransformerService : IDataTransformerService
    {
        private IIntentRepository _intentRepository;
        public DataTransformerService(IIntentRepository intentRepository)
        {
            _intentRepository = intentRepository;
        }
        public async Task<IEnumerable<Intent>> GetIntents()
        {
            return await _intentRepository.GetIntents();
        }

        public Task<List<string>> GetLables()
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<string>> GetWords()
        {
            var intents = await  GetIntents();
            return null;
        }
    }
}
