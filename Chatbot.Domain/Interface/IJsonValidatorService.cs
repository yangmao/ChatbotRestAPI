namespace Chatbot.Domain.Concrete
{
    public interface IJsonValidatorService
    {
        bool IsValidJson(string jsonString);
    }
}
