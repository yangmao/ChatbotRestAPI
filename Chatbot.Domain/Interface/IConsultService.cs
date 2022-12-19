using System.Threading.Tasks;

namespace Chatbot.Domain.Interface
{
    public interface IConsultService
    {
        public Task<string[]> Consult(string query);
    }
}