using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SupportiveMessageConsumer.Data;
using SupportiveMessageConsumer.Services;
using MongoDB.Driver;

public class Program
{
    public static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        TestDatabaseConnection(host);

        host.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });

    private static void TestDatabaseConnection(IHost host)
    {
        using (var scope = host.Services.CreateScope())
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
}