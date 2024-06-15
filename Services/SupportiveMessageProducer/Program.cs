using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SupportiveMessageProducer.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<RabbitMQPublisher>();
builder.Services.AddLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
});

var app = builder.Build();

app.UseAuthorization();
app.MapControllers();
app.Run();