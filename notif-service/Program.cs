using notif_service.Consumer;
using notif_service.Hubs;
using notif_service.Models;
using notif_service.Producer;
using notif_service.Profiles;
using notif_service.Services;
using notif_service.Services.Email;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();

var dbUser = Environment.GetEnvironmentVariable("DB_USER");
var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");
var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
var dbPort = Environment.GetEnvironmentVariable("DB_PORT");

Console.WriteLine($"DB_USER: {dbUser}");
Console.WriteLine($"DB_PASSWORD: {dbPassword}");
Console.WriteLine($"DB_HOST: {dbHost}");
Console.WriteLine($"DB_PORT: {dbPort}");

var mongoConnectionString = $"mongodb://{dbUser}:{dbPassword}@{dbHost}:{dbPort}";

Console.WriteLine($"MongoDB Connection String: {mongoConnectionString}");

builder.Services.Configure<NotificationDatabaseSettings>(options =>
{
    options.ConnectionString = mongoConnectionString;
    options.DatabaseName = "notifications-db";
    options.NotificationsCollectionName = "Notifications";
});
builder.Services.AddAutoMapper(typeof(NotificationProfile));
builder.Services.AddScoped<INotificationService,NotificationService>();
builder.Services.AddScoped<IEmailservice,EmailService>();
builder.Services.AddScoped<IRabbitMQProducer,RabbitMQProducer>();
builder.Services.AddControllers();
builder.Services.AddScoped<RabbitMQConsumer>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
using var scope = app.Services.CreateScope();
var rabbitMQConsumer = scope.ServiceProvider.GetRequiredService<RabbitMQConsumer>();
while (!rabbitMQConsumer.SetConnection()) ;
rabbitMQConsumer.StartConsuming();

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapHub<NotificationHub>("/notificationHub");

app.Run();
