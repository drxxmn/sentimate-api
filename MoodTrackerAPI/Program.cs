using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MoodTrackingService.Data;
using Microsoft.Extensions.Configuration; // Ensure this is included for IConfiguration

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register MongoDbContext
// Ensure you're injecting the concrete class, not the interface, if you're not using an interface
builder.Services.AddScoped<MongoDbContext>(provider =>
{
    // Obtain the configuration instance to access app settings
    var configuration = provider.GetRequiredService<IConfiguration>();
    return new MongoDbContext(configuration);
});

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();