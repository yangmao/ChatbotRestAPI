
using Chatbot.Domain;
using Chatbot.Domain.Concrete;
using Chatbot.Domain.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;



namespace TrainingDataProviderAPI
{
    [ApiController]
    public class TrainingDataController : ControllerBase
    {
        private readonly ILogger<TrainingDataController> _logger;
        private readonly IWordEmbeddingService _wordEmbeddingService;

        public TrainingDataController(IWordEmbeddingService wordEmbeddingService, ILogger<TrainingDataController> logger)
        {
            _logger = logger;
            _wordEmbeddingService = wordEmbeddingService;
        }

        [Route("/TrainingData")]
        public async Task<string> Get()
        {
            try
            {
                return await _wordEmbeddingService.GetTraining();
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Debug, new EventId(), null, ex);
                return "Sorry, there is an error. please contact administrator." + ex.Message;
            }
        }

        [Route("/BagOfWords")]
        public async Task<int[]> GetBagOfWords(string pattern)
        {
            try
            {
                var words = await _wordEmbeddingService.GetWords();
                return NLPHelper.BagOfWords(pattern, words);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Debug, new EventId(), null, ex);
                return new int[0];
            }
        }

        


    }
}