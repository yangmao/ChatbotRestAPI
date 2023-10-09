using Chatbot.Domain.Ports;
using Microsoft.Extensions.DependencyInjection;

namespace Database.SQLServer.Extensions
{
    public static class ServicesCollectionExtensions
    {
        public static void RegisterSqlServerDB(this IServiceCollection services)
        {
           
            services.AddSingleton<IDapperContext,DapperContext>();
        }
    }
}
