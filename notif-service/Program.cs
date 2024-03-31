using notif_service.Consumer;
using notif_service.Hubs;
using notif_service.Models;
using notif_service.Profiles;
using notif_service.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();

// Add services to the container.
/*
NotificationDatabaseSettings NotificationDatabase = new()
{
    ConnectionString = $"mongodb://{Environment.GetEnvironmentVariable("DB_USER")}:{Environment.GetEnvironmentVariable("DB_PASSWORD")}@{Environment.GetEnvironmentVariable("DB_HOST")}:{Environment.GetEnvironmentVariable("DB_PORT")}",
    DatabaseName = "notifications-service",
    NotificationsCollectionName = "Notifications"
};
builder.Services.Configure<NotificationDatabaseSettings>(options =>
{
    options.ConnectionString = NotificationDatabase.ConnectionString;
    options.DatabaseName = NotificationDatabase.DatabaseName;
    options.NotificationsCollectionName = NotificationDatabase.NotificationsCollectionName;
});*/
builder.Services.Configure<NotificationDatabaseSettings>(builder.Configuration.GetSection("NotificationDatabase"));
builder.Services.AddAutoMapper(typeof(NotificationProfile));
builder.Services.AddScoped<INotificationService,NotificationService>();
var emailConfig = builder.Configuration
        .GetSection("EmailConfiguration")
        .Get<MailSettings>();
builder.Services.AddSingleton(emailConfig);
builder.Services.AddTransient<IEmailService,EmailService>();
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
app.MapHub<NotificationHub>("/notificationHub");

app.Run();
