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
                wrds += " " + intent.Pattern;
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

            var training = new int[intents.Count()][];
            var output = new int[intents.Count()][];

            for (int i = 0; i < intents.Count(); i++)
            {
                var output_row = new int[intents.Count()];
                for (int j = 0; j < intents.Count(); j++)
                {
                    output_row[j] = 0;
                }
                output[i] = output_row;
                training[i] = NLPHelper.BagOfWords(intents[i].Pattern, words);
                output[i][labels.ToList().IndexOf(intents[i].Tag)] = 1;
            }

            return JsonConvert.SerializeObject(new { training = training, output = output });
        }
    }
}
