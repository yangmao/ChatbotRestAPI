using Chatbot.Domain.Ports;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace ChatbotRestAPI.Controller
{
    [ApiController]
    [Route("[controller]")]
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
        public async Task<IActionResult> Create(object json)
        {
            try
            {
                await _intentRepository.AddIntents(json.ToString());
                return Created("", "successfully created.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(object json)
        {
            try
            {
                await _intentRepository.UpsertIntent(json.ToString());
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var intents = await _intentRepository.GetIntents();
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
        public async Task<IActionResult> Delete(string tag)
        {
            try
            {
                await _intentRepository.RemoveIntent(tag);
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
