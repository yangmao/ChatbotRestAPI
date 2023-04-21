using Chatbot.Domain.Ports;
using Database.SQLServer.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Database.SQLServer.Extensions
{
    public static class ServicesCollectionExtensions
    {
        public static void RegisterSqlServerDB(this IServiceCollection services)
        {
           
            services.AddSingleton<IDapperContext,DapperContext>();
            services.AddScoped<IIntentRepository>(x=> new IntentRepository(services.BuildServiceProvider().GetService<IDapperContext>()));
        }
    }
}
