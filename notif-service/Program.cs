using notif_service.Consumer;
using notif_service.Models;
using notif_service.Profiles;
using notif_service.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<NotificationDatabaseSettings>(builder.Configuration.GetSection("NotificationDatabase"));
builder.Services.AddAutoMapper(typeof(NotificationProfile));
builder.Services.AddScoped<INotificationService,NotificationService>();
builder.Services.AddControllers();
builder.Services.AddScoped<RabbitMQConsumer>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
using var scope = app.Services.CreateScope();
var rabbitMQConsumer = scope.ServiceProvider.GetRequiredService<RabbitMQConsumer>();
while (!rabbitMQConsumer.SetConnection()) ;
await rabbitMQConsumer.StartConsuming();
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