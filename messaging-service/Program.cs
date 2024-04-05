using Amazon.S3;
using messaging_service.Consumer;
using messaging_service.Data;
using messaging_service.Exceptions;
using messaging_service.MappingProfiles;
using messaging_service.Repository;
using messaging_service.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
var myAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(option =>
{

    var dbHost = Environment.GetEnvironmentVariable("DB_HOST")?? "localhost";
    var dbName = Environment.GetEnvironmentVariable("DB_NAME") ?? "dev";
    var dbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD") ?? "";

    string connectionString = $"Server={dbHost},1433;Database={dbName};User Id=SA;Password={dbPassword};Trusted_Connection=false;TrustServerCertificate=True";
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

builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());
builder.Services.AddAWSService<IAmazonS3>();

builder.Services.AddControllers();
builder.Services.AddScoped<RabbitMQConsumer>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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


builder.Services.AddAutoMapper(typeof(MemberProfile),typeof(UserProfile),typeof(WorkspaceProfile),typeof(ChannelProfile),typeof(ChatProfile));
var app = builder.Build();
using var scope = app.Services.CreateScope();
var rabbitMQConsumer = scope.ServiceProvider.GetRequiredService<RabbitMQConsumer>();

while (!rabbitMQConsumer.SetConnection()) ;
rabbitMQConsumer.StartConsuming();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.UseStatusCodePages();
app.UseExceptionHandler();
app.MapControllers();
app.UseCors(myAllowSpecificOrigins);

ApplyMigration();
app.Run();

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
}
