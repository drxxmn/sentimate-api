using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SupportiveMessageProducer.Services;

var builder = WebApplication.CreateBuilder(args);


// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder
            .WithOrigins("http://your-frontend-url.com") // Replace with your frontend URL
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials());
});

builder.Services.AddControllers();
builder.Services.AddSingleton<RabbitMQPublisher>();
builder.Services.AddLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
});

var app = builder.Build();

app.UseCors("AllowSpecificOrigin"); // Add this line to enable CORS

app.UseAuthorization();
app.MapControllers();
app.Run();