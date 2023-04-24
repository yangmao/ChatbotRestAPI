using Chatbot.Domain.Extensions;
using Database.MongoDb.Adapter.Extentions;
using Database.SQLServer.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Chatbot.WearingUp.Extensions
{
    public static class ServicesCollectionExtensions
    {
        public static void RegisterChatbot(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDomainServices();
            services.RegisterMongoDB(configuration);
            //services.RegisterSqlServerDB();
        }
    }
}
