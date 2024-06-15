using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SupportiveMessageConsumer.Data;
using SupportiveMessageConsumer.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<MongoDbContext>();
builder.Services.AddSingleton<RabbitMQConsumer>();
builder.Services.AddHostedService<RabbitMQConsumer>();
builder.Services.AddLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
});

var app = builder.Build();

app.UseAuthorization();
app.MapControllers();
app.Run();
