using Chatbot.Domain.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Chatbot.Domain.Concrete
{
    public class ConsultService : IConsultService
    {
        private readonly IDataTransformerService _dataTransformerService;
        private readonly IHttpHandler _httpClient;
        public ConsultService(IDataTransformerService dataTransformerService, IHttpHandler httpClient)
        {
            _dataTransformerService = dataTransformerService;
            _httpClient = httpClient;
        }
        public async Task<string> Consult(string query)
        {
            var words = await _dataTransformerService.GetWords();
            var inputdata = NLPHelper.BagOfWords(query, words);

            var content = new StringContent(JsonConvert.SerializeObject(new { instances = new int[][] { inputdata } }), Encoding.UTF8, "application/json");
            var modelUrl = "https://chatbot-model-server-cr2e4b3nfa-ts.a.run.app/v1/models/chatbot_model:predict";
            var returnValue = await _httpClient.Client.PostAsync(modelUrl, content).Result.Content.ReadAsStringAsync();

            return await ProcessResponse(returnValue);
        }

        private async Task<string> ProcessResponse(string responseResult)
        {
            var resultObj = JsonConvert.DeserializeObject<Dictionary<string, List<List<double>>>>(responseResult);
            var pridictionList = resultObj["predictions"][0];
            var maxValue = pridictionList.Max(x => x);
            int? index = null;
            if (maxValue > 0.80)
            {
                index = pridictionList.IndexOf(maxValue);
            }
            var labels = await _dataTransformerService.GetLables();
            var theLabel = index != null ? (labels.ToList())[index.Value] : "unknown";
            var intents = await _dataTransformerService.GetIntents();
            var response = "";
            foreach (var intent in intents)
            {
                if (intent.Tag == theLabel)
                {
                    char[] delims = new char[] { '\"' };
                    var responseList = intent.Response.Split(delims, StringSplitOptions.RemoveEmptyEntries);
                    responseList = responseList.Where(x => x != "," && x !=", " ).ToArray();
                    Random rnd = new Random();
                    var randomChoice = rnd.Next(0, responseList.Length - 1);
                    response = responseList[randomChoice];
                }
            }
            return response;
        }
    }
}
