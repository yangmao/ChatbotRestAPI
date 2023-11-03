namespace Chatbot.Domain.Interface
{
    public interface IJsonValidatorService
    {
        bool IsValidJson(string jsonString);
    }
}
