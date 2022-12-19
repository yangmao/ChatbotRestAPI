using Chatbot.Domain.Interface;
using System;
using System.Net.Http;
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
            var httpClient = new HttpClient();
            var modelUrl = "https://chatbot-model.herokuapp.com/v1/models/chatbot_model:predict";
            var words = await _dataTransformerService.GetWords();
            var vector = NLPHelper.BagOfWords(query, words);
            return vector;
            
        }

       
    }
}
