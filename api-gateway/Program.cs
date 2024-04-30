using Ocelot.Middleware;
using Ocelot.DependencyInjection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Ocelot.Values;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("ocelot.json",optional: false, reloadOnChange: true)
    .AddJsonFile("Ocelot.auth.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.AddSignalR();
builder.Services.AddOcelot(builder.Configuration);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.Authority = "https://auth-service";
            options.RequireHttpsMetadata = false;
            options.Audience = "stack-services"; 
        });

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

// TODO: Add Swagger

var app = builder.Build();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
await app.UseOcelot();

app.Run();
