using Chatbot.Domain.Ports;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Chatbot.Domain.Interface;
using Newtonsoft.Json;
using Chatbot.Domain.Models;

namespace ChatbotRestAPI.Controller
{
    [ApiController]

    public class IntentController : ControllerBase
    {
        private readonly ILogger<IntentController> _logger;
        private readonly IIntentRepository _intentRepository;
        private readonly IJsonValidatorService _jsonValidatorService;

        public IntentController(IIntentRepository intentRepository, ILogger<IntentController> logger, IJsonValidatorService jsonValidatorService)
        {
            _intentRepository = intentRepository;
            _logger = logger;
            _jsonValidatorService = jsonValidatorService;
        }

        [HttpPost]
        [Route("UpsertAll")]
        public async Task<IActionResult> Create(string userId, object json)
        {
            try
            {
                if (_jsonValidatorService.IsValidJson(json.ToString()))
                {
                    await _intentRepository.UpsertIntent(userId, json.ToString());
                    var intentsObject = await _intentRepository.GetIntents(userId);
                    return Created("/Create", intentsObject);
                }
                else
                {
                    return BadRequest("Invalid JSON format");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("CreateOne")]
        public async Task<IActionResult> CreateOne(string userId, object json)
        {
            try
            {
                if (_jsonValidatorService.IsValidJson(json.ToString()))
                {
                    var intent = JsonConvert.DeserializeObject<Intent>(json.ToString());

                    // Check for tag uniqueness when adding
                    if (string.IsNullOrEmpty(intent.Tag) || await IsTagAlreadyExists(userId, intent.Tag))
                    {
                        return BadRequest("Invalid or duplicate tag detected. Please provide unique and non-empty tags.");
                    }

                    await _intentRepository.UpsertOne(userId, json.ToString());

                    var intentsObject = await _intentRepository.GetIntents(userId);
                    return Created("/CreateOne", intentsObject);
                }
                else
                {
                    return BadRequest("Invalid JSON format");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("UpdateOne")]
        public async Task<IActionResult> Update(string userId, object json)
        {
            try
            {
                if (_jsonValidatorService.IsValidJson(json.ToString()))
                {
                    await _intentRepository.UpsertIntent(userId, json.ToString());
                    return Ok();
                }
                else
                {
                    return BadRequest("Invalid JSON format");
                }
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
        private async Task<bool> IsTagAlreadyExists(string userId, string tag)
        {
            var intents = await _intentRepository.GetIntents(userId);
            return intents.Any(intent => intent.Tag == tag);
        }
    }
}
