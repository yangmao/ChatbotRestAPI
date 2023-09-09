
namespace Database.MongoDb.Adapter.Models
{
    public interface IChatbotMongoDdatabaseSettings
    {
        public string CollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
