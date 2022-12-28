using Chatbot.Domain.Interface;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Chatbot.Domain.concrete
{
    public class ConsultService : IConsultService
    {
        private readonly IDataTransformerService _dataTransformerService;
        public ConsultService(IDataTransformerService dataTransformerService)
        {
            _dataTransformerService = dataTransformerService;
        }
        public async Task<string[]> Consult(string query)
        {
            var words = await _dataTransformerService.GetWords();
            var reply = NLPHelper.BagOfWords(query, words);
            var content = new StringContent(JsonConvert.SerializeObject(reply), Encoding.UTF8, "application/json");

            var httpClient = new HttpClient();
            var modelUrl = "https://chatbot-model.herokuapp.com/v1/models/chatbot_model:predict";
            await httpClient.PostAsync(modelUrl,content);
            
            return reply;
            
        }

       
    }
}
