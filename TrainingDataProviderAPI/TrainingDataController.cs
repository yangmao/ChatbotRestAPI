
using Chatbot.Domain.Concrete;
using Chatbot.Domain.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;



namespace TrainingDataProviderAPI
{
    [ApiController]
    [Route("[controller]")]
    public class TrainingDataController : ControllerBase
    {
        private readonly ILogger<TrainingDataController> _logger;
        private readonly IDataTransformerService _dataTransformerService;

        public TrainingDataController(IDataTransformerService dataTransformerService, ILogger<TrainingDataController> logger)
        {
            _logger = logger;
            _dataTransformerService = dataTransformerService;
        }

        public async Task<string> Get()
        {
            return  await _dataTransformerService.GetTraining();
        }

        


    }
}