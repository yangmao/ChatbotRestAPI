using Chatbot.Domain.Interface;
using Newtonsoft.Json;
using System;
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
            var reply = NLPHelper.BagOfWords(query, words);

            var content = new StringContent(JsonConvert.SerializeObject(new { instances = new int[][] { reply } }), Encoding.UTF8, "application/json");
            var temp2 = new int[][] { reply };
            var temp = JsonConvert.SerializeObject(new { instances = new int[][] { reply } });
            var modelUrl = "http://localhost:8501/v1/models/chatbot_model:predict";
            var returnValue =await _httpClient.Client.PostAsync(modelUrl,content).Result.Content.ReadAsStringAsync();
            
            return returnValue;
            
        }

       
    }
}
