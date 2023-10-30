using Chatbot.Domain.Ports;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ChatbotRestAPI.Controller
{
    [ApiController]
    
    public class IntentController : ControllerBase
    {
        private readonly ILogger<IntentController> _logger;
        private readonly IIntentRepository _intentRepository;

        public IntentController(IIntentRepository intentRepository, ILogger<IntentController> logger)
        { 
            _intentRepository = intentRepository;
            _logger = logger;
        }

        [HttpPost]
        [Route("UpsertAll")]
        public async Task<IActionResult> Create(string userId, object json)
        {
            try
            {
                await _intentRepository.AddIntents(userId,json.ToString());
                var intentsObject = await _intentRepository.GetIntents(userId);
                return Created("/UpsertAll", intentsObject);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("UpsertOne")]
        public async Task<IActionResult> Upsert(string userId, object json)
        {
            try
            {
                await _intentRepository.UpsertIntent(userId,json.ToString());
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> Get(string userId)
        {
            try
            {
                var intents = await _intentRepository.GetIntents(userId);
                if (intents == null)
                    return NotFound();
                return Ok(intents);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("DeleteOne")]
        public async Task<IActionResult> Delete(string userId, string tag)
        {
            try
            {
                await _intentRepository.RemoveIntent(userId,tag);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }
    }
}
