using Chatbot.Domain.Interface;
using Chatbot.Domain.Ports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatbot.Domain.Concrete
{
    public class CompanyService : ICompanyService
    {
        private IIntentRepository _intentRepository;
        public CompanyService(IIntentRepository intentRepository)
        {
            _intentRepository = intentRepository;
        }
        public Task SetTitle(string title)
        {
           throw new Exception();
        }
    }
}
