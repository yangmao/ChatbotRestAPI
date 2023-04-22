using Chatbot.Domain.Interface;
using Chatbot.Domain.Models;
using Chatbot.Domain.Ports;
using Newtonsoft.Json;
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
        public async Task<List<Intent>> GetIntents()
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
                foreach(var pattern in intent.Pattern)
                    wrds += " " + pattern;
            }

            var words = NLPHelper.Tokenize(wrds);
            words = words
                .Select(x => (x.Contains("\"") ? x.Replace("\"", "").ToLower() : x.ToLower()))
                .Distinct()
                .ToArray();
            words = NLPHelper.Stemmerize(words);
            words = words.Distinct().Where(x => x != "").ToArray();
            Array.Sort(words);
            return words;
        }

        public async Task<string> GetTraining()
        {
            
            var intents = await GetIntents();
            var words = await GetWords();
            var labels = await GetLables();

            var training = new List<int[]>();
            var output = new List<int[]>();

            foreach (var intent in intents)
            {
                //var patterns = JsonConvert.DeserializeObject<List<string>>(intent.Pattern);
                foreach (var pattern in intent.Pattern)
                {
                    var output_row = new int[labels.Count()];
                    output_row[labels.ToList().IndexOf(intent.Tag)] = 1;
                    training.Add(NLPHelper.BagOfWords(pattern, words));
                    output.Add(output_row);
                } 
            }

            return JsonConvert.SerializeObject(new { training = training, output = output });
        }
    }
}
