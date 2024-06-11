var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHostedService<RabbitMQConsumer>();
builder.Services.AddControllers();

var app = builder.Build();
app.UseAuthorization();
app.MapControllers();
app.Run();