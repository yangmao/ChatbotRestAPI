using Chatbot.WearingUp.Extensions;
using ChatbotRestAPI.Middleware;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHealthChecks();
builder.Services.AddSwaggerGen(x =>
{
    x.AddSecurityDefinition("X-API-KEY", new OpenApiSecurityScheme
    {
        Name = "X-API-KEY",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "ApiKeyScheme",
        In = ParameterLocation.Header,
        Description = "ApiKey must appear in header"
    });
    x.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "X-API-KEY"
                },
                In = ParameterLocation.Header
            },
            new string[]{}
        }
    });
});
var corsPolicy = "AllowOrigin";
// Add services to the container.
builder.Services.AddCors(c =>
{
    //c.AddPolicy(corsPolicy, options => options.AllowAnyOrigin().AllowAnyHeader());
    c.AddPolicy("AllowOrigin",
                     policy =>
                     {
                         policy.AllowAnyHeader().WithOrigins("https://localhost:44376");
                     });
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.RegisterChatbot(builder.Configuration);


var app = builder.Build();
app.MapHealthChecks("/health");
app.UseCors(corsPolicy);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "MyAPI");
        options.RoutePrefix = string.Empty;
    });
}
app.UseHttpsRedirection();
app.UseMiddleware<ApiKeyMiddleware>();
app.UseAuthorization();

app.MapControllers();

app.Run();
