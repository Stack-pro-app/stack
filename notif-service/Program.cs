using Microsoft.OpenApi.Models;
using notif_service.Consumer;
using notif_service.Hubs;
using notif_service.Models;
using notif_service.Producer;
using notif_service.Profiles;
using notif_service.Services;
using notif_service.Services.Email;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var myAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins,
                      policy =>
                      {
                        policy.WithOrigins("http://localhost:4200") // Change port to 4200
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials();
                      });
});
builder.Services.AddSignalR();


var dbUser = Environment.GetEnvironmentVariable("DB_USER");
var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");
var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
var dbPort = Environment.GetEnvironmentVariable("DB_PORT");

var mongoConnectionString = $"mongodb://{dbUser}:{dbPassword}@{dbHost}:{dbPort}";

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
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Messaging APIs",
        Description = "This the documentation for The Messaging Service Apis",
    });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();
using var scope = app.Services.CreateScope();
var rabbitMQConsumer = scope.ServiceProvider.GetRequiredService<RabbitMQConsumer>();
while (!rabbitMQConsumer.SetConnection()) ;
rabbitMQConsumer.StartConsuming();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(myAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();
app.MapHub<NotificationHub>("/notificationHub");

app.Run();
