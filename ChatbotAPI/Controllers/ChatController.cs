
using Chatbot.Domain;
using Chatbot.Domain.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        public async Task<string[]> Chat(string msg)
        {
          
            return await _consultService.Consult(msg);
        }
    }
}
