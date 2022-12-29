using Chatbot.Domain.concrete;
using Chatbot.Domain.Interface;
using Chatbot.Domain.Ports;
using Microsoft.Extensions.DependencyInjection;

namespace Chatbot.Domain.Extensions
{
    public static class ServicesCollectionExtensions
    {
        public static void AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<IDataTransformerService>(x =>
               new DataTransformerService(services.BuildServiceProvider().GetService<IIntentRepository>()));
            services.AddScoped<IConsultService>(x =>
               new ConsultService(services.BuildServiceProvider().GetService<IDataTransformerService>()));


        }
    }
}
