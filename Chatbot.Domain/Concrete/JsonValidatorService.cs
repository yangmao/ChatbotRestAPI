using Chatbot.Domain.Interface;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Chatbot.Domain.Concrete
{
    public class JsonValidatorService : IJsonValidatorService
    {
        public bool IsValidJson(string jsonString)
        {
            try
            {
                JToken.Parse(jsonString);
                return true;
            }
            catch (JsonReaderException)
            {
                return false;
            }
        }
    }
}
