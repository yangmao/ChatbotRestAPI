using System;
using System.Collections.Generic;
using System.Text;

namespace Chatbot.Domain.Models
{
    public class Intent
    {
        public string Tag { get; set; }
        public List<string> Pattern { get; set; }
        public List<string> Response { get; set; }
    }
}
