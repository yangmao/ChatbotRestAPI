using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Chatbot.Domain.Services
{
    public interface IJsonValidatorService
    {
        bool IsValidJson(string jsonString);
    }

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
