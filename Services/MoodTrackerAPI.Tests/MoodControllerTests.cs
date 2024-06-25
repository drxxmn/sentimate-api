using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace MoodTrackerAPI.Tests
{
    public class MoodControllerTests
    {
        private readonly HttpClient _client;

        public MoodControllerTests()
        {
            // Arrange
            var builder = WebApplication.CreateBuilder(new string[] { }); // Pass an empty string array for args

            // Configure services as needed (e.g., MongoDB context, CORS, etc.)
            ConfigureTestServices(builder);

            // Build the application
            var app = builder.Build();

            // Create TestServer and HttpClient
            var server = new TestServer(new WebApplicationBuilder().UseStartup<Startup>());
            _client = server.CreateClient();
        }

        private void ConfigureTestServices(WebApplication builder)
        {
            // Set up MongoDB context if needed (adjust as per your application)
            builder.Services.AddScoped<MongoDbContext>(provider =>
            {
                // Provide necessary configuration if required for tests
                // For testing, you might want to mock or use in-memory database
                // Example:
                // return new MongoDbContext(mockedConfiguration);
                return null; // Replace with appropriate setup or remove if not needed
            });

            // Configure CORS (if needed)
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder
                        .WithOrigins("http://your-frontend-url.com") // Replace with your frontend URL
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials());
            });

            // Add any other services needed for testing
        }

        [Fact]
        public async Task Get_ReturnsOk()
        {
            // Act
            var response = await _client.GetAsync("/api/mood");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            // Add more assertions based on your test requirements
        }
    }
}
