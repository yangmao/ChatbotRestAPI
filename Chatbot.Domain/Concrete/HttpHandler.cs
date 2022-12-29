using Chatbot.Domain.Interface;
using System.Net.Http;

namespace Chatbot.Domain.Concrete
{
    public class HttpHandler : IHttpHandler
    {
        public HttpClient Client
        {
            get
            {
                return new HttpClient();
            }
        }
    }
}
