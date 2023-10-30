using Chatbot.Domain.Ports;
<<<<<<< HEAD
using Microsoft.AspNetCore.Authorization;
=======
>>>>>>> origin/Test
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
<<<<<<< HEAD
        public async Task<IActionResult> Create(object json)
        {
            try
            {
                await _intentRepository.AddIntents(json.ToString());
                return Created("", "successfully created.");
=======
        public async Task<IActionResult> Create(string userId, object json)
        {
            try
            {
                await _intentRepository.AddIntents(userId,json.ToString());
                var intentsObject = await _intentRepository.GetIntents(userId);
                return Created("/UpsertAll", intentsObject);
>>>>>>> origin/Test
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
<<<<<<< HEAD
                return BadRequest(ex.Message);
=======
                return BadRequest();
>>>>>>> origin/Test
            }
        }

        [HttpPut]
        [Route("UpsertOne")]
<<<<<<< HEAD
        public async Task<IActionResult> Update(object json)
        {
            try
            {
                await _intentRepository.UpsertIntent(json.ToString());
=======
        public async Task<IActionResult> Upsert(string userId, object json)
        {
            try
            {
                await _intentRepository.UpsertIntent(userId,json.ToString());
>>>>>>> origin/Test
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
<<<<<<< HEAD
                return BadRequest(ex.Message);
=======
                return BadRequest();
>>>>>>> origin/Test
            }
        }

        [HttpGet]
        [Route("GetAll")]
<<<<<<< HEAD
        public async Task<IActionResult> Get()
        {
            try
            {
                var intents = await _intentRepository.GetIntents();
=======
        public async Task<IActionResult> Get(string userId)
        {
            try
            {
                var intents = await _intentRepository.GetIntents(userId);
>>>>>>> origin/Test
                if (intents == null)
                    return NotFound();
                return Ok(intents);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
<<<<<<< HEAD
                return BadRequest(ex.Message);
=======
                return BadRequest();
>>>>>>> origin/Test
            }
        }

        [HttpDelete]
        [Route("DeleteOne")]
<<<<<<< HEAD
        public async Task<IActionResult> Delete(string tag)
        {
            try
            {
                await _intentRepository.RemoveIntent(tag);
=======
        public async Task<IActionResult> Delete(string userId, string tag)
        {
            try
            {
                await _intentRepository.RemoveIntent(userId,tag);
>>>>>>> origin/Test
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
<<<<<<< HEAD
                return BadRequest(ex.Message);
=======
                return BadRequest();
>>>>>>> origin/Test
            }
        }
    }
}
