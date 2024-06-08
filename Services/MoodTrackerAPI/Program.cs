using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;
using Grafana.OpenTelemetry;
using MoodTrackingService.Data;

var builder = WebApplication.CreateBuilder(args);

// Set up MongoDB context
builder.Services.AddScoped<MongoDbContext>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    return new MongoDbContext(configuration);
});

// grafana
using var tracerProvider = Sdk.CreateTracerProviderBuilder()
    .UseGrafana()
    .Build();
using var meterProvider = Sdk.CreateMeterProviderBuilder()
    .UseGrafana()
    .Build();
using var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddOpenTelemetry(logging =>
    {
        logging.UseGrafana();
    });
});
// grafana end 

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.Run();