using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Chatbot.Domain.Services
{
    public class JsonValidatorService
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
