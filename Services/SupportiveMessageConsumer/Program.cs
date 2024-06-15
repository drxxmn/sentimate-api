using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SupportiveMessageConsumer.Data;
using SupportiveMessageConsumer.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("ConnectionStrings"));
builder.Services.Configure<RabbitMQSettings>(builder.Configuration.GetSection("RabbitMQ"));
builder.Services.AddSingleton<MongoDbContext>();
builder.Services.AddHostedService<RabbitMQConsumer>();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();

app.MapControllers();

TestDatabaseConnection(app);

app.Run();

void TestDatabaseConnection(IHost app)
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var logger = services.GetRequiredService<ILogger<Program>>();
        try
        {
            var dbContext = services.GetRequiredService<MongoDbContext>();
            var collection = dbContext.SupportiveMessages;
            logger.LogInformation("Successfully connected to MongoDB and accessed the SupportiveMessages collection.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while testing the database connection.");
        }
    }
}