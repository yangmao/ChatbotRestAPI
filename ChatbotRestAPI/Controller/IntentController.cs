using Chatbot.Domain.Interface;
using Chatbot.Domain.Ports;
using ChatbotAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            //await _intentRepository.UpsertAsync(json.ToString());
            await _intentRepository.AddIntents(json.ToString());
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            await _intentRepository.GetIntents();
            return Ok();
        }
    }
}
