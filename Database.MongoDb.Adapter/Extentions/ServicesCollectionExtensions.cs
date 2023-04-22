using Chatbot.Domain.Ports;
using Database.MongoDb.Adapter.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Database.MongoDb.Adapter.Extentions
{
    public static class ServicesCollectionExtensions
    {
        public static void RegisterMongoDB(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ChatbotMongoDatabaseSettings>(configuration.GetSection(nameof(ChatbotMongoDatabaseSettings)));
            services.AddSingleton<IChatbotMongoDdatabaseSettings>(sp => sp.GetRequiredService<IOptions<ChatbotMongoDatabaseSettings>>().Value);
            services.AddSingleton<IMongoClient>(s => new MongoClient(configuration.GetValue<string>("ChatbotMongoDatabaseSettings:ConnectionString")));
            services.AddScoped<IIntentContext, IntentsContext>();
            services.AddScoped<IIntentRepository>(x => new IntentRepository(services.BuildServiceProvider().GetService<IIntentContext>()));
        }
    }
}
