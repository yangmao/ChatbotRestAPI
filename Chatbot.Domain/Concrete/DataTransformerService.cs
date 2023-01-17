using Chatbot.Domain.Interface;
using Chatbot.Domain.Models;
using Chatbot.Domain.Ports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Chatbot.Domain.Concrete
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

        public async Task<string[]> GetLables()
        {
            var intents = await GetIntents();
            var labels = new List<string>();
            foreach (var intent in intents)
            {
                labels.Add(intent.Tag);
            }
            var lbls = labels.ToArray();
            Array.Sort(lbls);
            return lbls;
        }

        public async Task<string[]> GetWords()
        {
            var intents = await GetIntents();
            string wrds = string.Empty;
            foreach (var intent in intents)
            {
                wrds += " " + intent.Pattern;
            }

            var words = NLPHelper.Tokenize(wrds);
            words = words
                .Select(x => (x.Contains("\"") ? x.Replace("\"", "").ToLower() : x.ToLower()))
                .Distinct()
                .ToArray();
            words = NLPHelper.Stemmerize(words);
            words = words.Distinct().Where(x=>x !="").ToArray();
            Array.Sort(words);
            return words;
        }
    }
}
