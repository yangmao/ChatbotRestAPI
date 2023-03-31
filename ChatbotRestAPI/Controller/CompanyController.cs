using Chatbot.Domain.Concrete;
using Chatbot.Domain.Interface;
using ChatbotAPI.Controllers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatbotRestAPI.Controller
{
    public class CompanyController : ControllerBase
    {
        private readonly ILogger<ChatController> _logger;
        private readonly ICompanyService _companyService;
        public CompanyController(ICompanyService companyService, ILogger<ChatController> logger)
        {
            _logger = logger;
            _companyService = companyService;
        }
        [HttpPost]
        [EnableCors("AllowOrigin")]
        public async Task<string> SendTitle(string title)
        {
            throw new Exception();
        }
    }
}
