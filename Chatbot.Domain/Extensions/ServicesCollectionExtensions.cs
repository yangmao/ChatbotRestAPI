using Chatbot.Domain.Concrete;
using Chatbot.Domain.Interface;
using Chatbot.Domain.Ports;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

namespace Chatbot.Domain.Extensions
{
    public static class ServicesCollectionExtensions
    {
        public static void AddDomainServices(this IServiceCollection services)
        {
            
            services.AddScoped<IDataTransformerService>(x =>
               new DataTransformerService(services.BuildServiceProvider().GetService<IIntentRepository>()));
            services.AddSingleton<IHttpHandler, HttpHandler>();
            services.AddScoped<IConsultService>(x =>
               new ConsultService(services.BuildServiceProvider().GetService<IDataTransformerService>(), services.BuildServiceProvider().GetService<IHttpHandler>(), services.BuildServiceProvider().GetService<IConfiguration>()));



        }
    }
}
