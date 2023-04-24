using System.Collections.Generic;

namespace Chatbot.Domain.Models
{
    public class IntentDto
    {
        public string Tag { get; set; }
        
        public List<string> Pattern { get; set; }

        public List<string> Response { get; set; }
    }
}
