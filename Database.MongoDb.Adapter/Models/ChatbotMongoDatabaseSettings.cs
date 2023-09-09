
namespace Database.MongoDb.Adapter.Models
{
    public class ChatbotMongoDatabaseSettings : IChatbotMongoDdatabaseSettings
    {
        public string CollectionName { get; set; } = string.Empty;
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
    }
}
