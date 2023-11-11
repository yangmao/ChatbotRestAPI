using Chatbot.Domain.Ports;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Chatbot.Domain.Interface;
using Chatbot.Domain.Models;
using Newtonsoft.Json;

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
                    await _intentRepository.AddIntents(userId, json.ToString());
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
        [Route("AddOne")]
        public async Task<IActionResult> Add(string userId, object json)
        {
            try
            {
                if (_jsonValidatorService.IsValidJson(json.ToString()))
                {
                    var tag = JsonConvert.DeserializeObject<Intent>(json.ToString()).Tag;
                    var existingIntent = await _intentRepository.GetIntents(userId);
                    if (existingIntent.Any(x => x.Tag == tag) || string.IsNullOrEmpty(tag))
                    {
                        return BadRequest("Tag must be unique and not empty. Please choose a different tag.");
                    }

                    await _intentRepository.UpsertIntent(userId, json.ToString()); // Call UpsertIntent instead of AddIntents
                    var intentsObject = await _intentRepository.GetIntents(userId);
                    return Created("/AddOne", intentsObject);
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
                await _intentRepository.UpsertIntent(userId, json.ToString());
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
