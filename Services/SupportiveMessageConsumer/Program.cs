using Microsoft.EntityFrameworkCore;
using SupportiveMessageConsumer.Data;
using SupportiveMessageConsumer.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSingleton<RabbitMQConsumer>();
builder.Services.AddHostedService(sp => sp.GetRequiredService<RabbitMQConsumer>());
builder.Services.AddControllers();

var app = builder.Build();
app.UseAuthorization();
app.MapControllers();
app.Run();