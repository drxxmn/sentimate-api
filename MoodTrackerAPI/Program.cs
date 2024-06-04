using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using MoodTrackingService.Data;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

// Set up MongoDB context
builder.Services.AddScoped<MongoDbContext>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    return new MongoDbContext(configuration);
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// OpenTelemetry Configuration
builder.Services.AddOpenTelemetryTracing(b => b
    .AddAspNetCoreInstrumentation()
    .AddHttpClientInstrumentation()
    .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("MoodTrackingService"))
    .AddGenevaExporter(o =>
    {
        o.ConnectionString = "InstrumentationKey=your-instrumentation-key";
    }));

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