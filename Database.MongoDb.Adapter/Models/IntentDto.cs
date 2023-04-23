using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.MongoDb.Adapter.Models
{
    public class IntentDto
    {
        public string Tag { get; set; }
        public List<string> Pattern { get; set; }
        public List<string> Response { get; set; }
    }
}
