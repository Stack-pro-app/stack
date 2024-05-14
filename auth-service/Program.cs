using auth_service.Data;
using auth_service.Models;
using auth_service.Producer;
using auth_service.Services;
using auth_service.Services.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name:MyAllowSpecificOrigins,
        policy =>
        {
            policy
                .WithOrigins("http://localhost:4200")
                .AllowCredentials()
                .AllowAnyHeader()
                .AllowAnyMethod();
                
        });
});

builder.Services.AddDbContext<AppDbContext>(option =>
{
    var dbHost = Environment.GetEnvironmentVariable("DB_HOST")?? "localhost";
    var dbName = Environment.GetEnvironmentVariable("DB_NAME") ?? "dev";
    var dbPort = Environment.GetEnvironmentVariable("DB_PORT") ?? "1433";
    var dbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD") ?? "";

    string connectionString = $"Server={dbHost},{dbPort};Database={dbName};User Id=SA;Password={dbPassword};Trusted_Connection=false;TrustServerCertificate=True";
    option.UseSqlServer(connectionString);
} );
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("ApiSettings:JwtOptions"));
builder.Services.AddScoped<IRabbitMQProducer, RabbitMQProducer>();
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddControllers();
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
using var scope = app.Services.CreateScope();

app.UseSwagger();
app.UseSwaggerUI();

var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
await dbContext.Database.MigrateAsync();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseCors(MyAllowSpecificOrigins);
app.Run();
