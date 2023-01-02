using Chatbot.Domain.Interface;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ChatbotAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly ILogger<ChatController> _logger;
        private readonly IConsultService _consultService;
        public ChatController(IConsultService consultService, ILogger<ChatController> logger)
        {
            _logger = logger;
            _consultService = consultService;
        }

        [HttpGet]
        public ObjectResult Get()
        {
            return new OkObjectResult("ok");
        }

        [HttpPost]
        [EnableCors("AllowOrigin")]
        public async Task<string[]> Chat(string msg)
        {
            try
            {
                return await _consultService.Consult(msg);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Debug, new EventId(), null, ex);
                return new string[] { "Sorry, there is an error. please contact administrator." };
            }
        }
    }
}
