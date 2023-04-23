using System;
using System.Collections.Generic;
using System.Text;

namespace Chatbot.Domain.Models
{
    public class Intent
    {
        public string Tag { get; set; }
        public string Pattern { get; set; }
        public string Response { get; set; }
    }
}
