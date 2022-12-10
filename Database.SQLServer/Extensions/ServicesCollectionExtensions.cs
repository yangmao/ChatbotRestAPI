using Chatbot.Domain.Ports;
using Database.SQLServer.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Database.SQLServer.Extensions
{
    public static class ServicesCollectionExtensions
    {
        public static void AddCustomDapperContext(this IServiceCollection services)
        { 
            services.AddSingleton<DapperContext>();
            services.AddScoped<IIntentRepository, IntentRepository>();
        }
    }
}
