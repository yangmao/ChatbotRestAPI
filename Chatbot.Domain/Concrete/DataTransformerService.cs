using Chatbot.Domain.Interface;
using Chatbot.Domain.Models;
using Chatbot.Domain.Ports;
using System;
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

        public Task<string[]> GetLables()
        {
            throw new System.NotImplementedException();
        }

        public async Task<string[]> GetWords()
        {
            var intents = await GetIntents();
            string wrds = string.Empty;
            foreach (var intent in intents)
            {
                wrds += " " + intent;
            }
            var words = NLPHelper.Tokenize(wrds);
            words = NLPHelper.Stemmerize(words);
            Array.Sort(words);
            return words;
        }
    }
}
