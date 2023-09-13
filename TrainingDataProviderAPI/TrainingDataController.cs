using Chatbot.Domain;
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

        [HttpGet]
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

        [HttpGet]
        [Route("/SentenceInBOW")]
        public async Task<int[]> GetSentenceInBOW(string pattern)
        {
            try
            {
                var words = await _wordEmbeddingService.GetVacabulary();
                return NLPHelper.BagOfWords(pattern, words);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Debug, new EventId(), null, ex);
                return new int[0];
            }
        }

        [HttpGet]
        [Route("/VocabularyCount")]
        public async Task<int> GetVocabularyCount()
        {
            try
            {
                var words = await _wordEmbeddingService.GetVacabulary();
                return words.Length;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Debug, new EventId(), null, ex);
                return 0;
            }
        }

        [HttpGet]
        [Route("/LabelsCount")]
        public async Task<int> GetLabelCount()
        {
            try
            {
                var labels = await _wordEmbeddingService.GetLables();
                return labels.Length;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Debug, new EventId(), null, ex);
                return 0;
            }
        }


    }
}