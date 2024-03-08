using messaging_service.Consumer;
using messaging_service.Data;
using messaging_service.MappingProfiles;
using messaging_service.Repository;
using messaging_service.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
var myAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);


var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
var dbName = Environment.GetEnvironmentVariable("DB_NAME");
var dbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");
var connectionString = $"Data Source={dbHost};Initial Catalog={dbName};User ID=sa;Password={dbPassword};Trusted_Connection=True;TrustServerCertificate=True;";
// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString(connectionString), sqlServerOptionsAction: sqlOptions =>
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

builder.Services.AddControllers();
builder.Services.AddScoped<RabbitMQConsumer>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<WorkspaceRepository>();
builder.Services.AddScoped<ChatRepository>();
builder.Services.AddScoped<ChannelRepository>();


builder.Services.AddAutoMapper(typeof(MemberProfile),typeof(UserProfile),typeof(WorkspaceProfile),typeof(ChannelProfile),typeof(ChatProfile));
var app = builder.Build();
using var scope = app.Services.CreateScope();
var rabbitMQConsumer = scope.ServiceProvider.GetRequiredService<RabbitMQConsumer>();
rabbitMQConsumer.SetConnection();
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
ApplyMigration();
app.UseCors(myAllowSpecificOrigins);

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
