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
            
            services.AddScoped<IWordEmbeddingService>(x =>
               new WordEmbeddingService(services.BuildServiceProvider().GetService<IIntentRepository>()));
            services.AddSingleton<IHttpHandler, HttpHandler>();
            services.AddSingleton<IJsonValidatorService, JsonValidatorService>();
            services.AddScoped<IConsultService>(x =>
               new ConsultService(services.BuildServiceProvider().GetService<IWordEmbeddingService>(), services.BuildServiceProvider().GetService<IHttpHandler>(), services.BuildServiceProvider().GetService<IConfiguration>()));



        }
    }
}
