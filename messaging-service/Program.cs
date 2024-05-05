using Amazon.Extensions.NETCore.Setup;
using Amazon.S3;
using messaging_service.Consumer;
using messaging_service.Data;
using messaging_service.Exceptions;
using messaging_service.Filters;
using messaging_service.MappingProfiles;
using messaging_service.Producer;
using messaging_service.Repository;
using messaging_service.Repository.Interfaces;
using messaging_service.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Reflection;
var myAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

var dbHost = Environment.GetEnvironmentVariable("DB_HOST") ?? "localhost";
var dbName = Environment.GetEnvironmentVariable("DB_NAME") ?? "messaging-db";
var dbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD") ?? "";

string connectionString = $"Server={dbHost},1433;Database={dbName};User Id=SA;Password={dbPassword};Trusted_Connection=false;TrustServerCertificate=True";

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(option =>
{
    //string connectionString = "Server=localhost;Database=messaging-db;Trusted_Connection=True;TrustServerCertificate=True";
    option.UseSqlServer(connectionString, sqlServerOptionsAction: sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure();
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins,
                      policy =>
                      {
                          policy.AllowAnyOrigin()
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                      });
});

AWSOptions awsOptions = new();

awsOptions.Credentials = new Amazon.Runtime.BasicAWSCredentials(Environment.GetEnvironmentVariable("AWS_ACCESS_KEY_ID"), Environment.GetEnvironmentVariable("AWS_SECRET_ACCESS_KEY"));
awsOptions.Region = Amazon.RegionEndpoint.USEast1;

builder.Services.AddDefaultAWSOptions(awsOptions);
builder.Services.AddAWSService<IAmazonS3>();
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
builder.Services.AddHealthChecks();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IInvitationService,InvitationService>();
builder.Services.AddScoped<IInvitationRepository,InvitationRepository>()
    .AddProblemDetails()
    .AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddScoped<IUserRepository,UserRepository>()
    .AddProblemDetails()
    .AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddScoped<IWorkspaceRepository,WorkspaceRepository>()
    .AddProblemDetails()
    .AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddScoped<IChatRepository,ChatRepository>()
    .AddProblemDetails()
    .AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddScoped<IChannelRepository,ChannelRepository>()
    .AddProblemDetails()
    .AddExceptionHandler<GlobalExceptionHandler>();


builder.Services.AddAutoMapper(typeof(MemberProfile),typeof(UserProfile),typeof(WorkspaceProfile),typeof(ChannelProfile),typeof(ChatProfile),typeof(InvitationProfile));
var app = builder.Build();
using var scope = app.Services.CreateScope();
var rabbitMQConsumer = scope.ServiceProvider.GetRequiredService<RabbitMQConsumer>();

while (!rabbitMQConsumer.SetConnection()) ;
rabbitMQConsumer.StartConsuming();


app.UseHealthChecks("/health");
app.UseSwagger();
app.UseSwaggerUI();
var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
await dbContext.Database.MigrateAsync();
    
app.UseAuthorization();
app.UseStatusCodePages();
app.UseExceptionHandler();
app.MapControllers();
app.UseCors(myAllowSpecificOrigins);


app.Run();
/*
void ApplyMigration()
{
    using (var scope = app.Services.CreateScope())
    {
        var _db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        if (_db.Database.GetPendingMigrations().Count() > 0)
        {
            _db.Database.Migrate();
        }
    }
}*/
