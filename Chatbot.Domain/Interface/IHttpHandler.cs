using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Chatbot.Domain.Interface
{
    public interface IHttpHandler
    {
        HttpClient Client { get; }
    }
}
