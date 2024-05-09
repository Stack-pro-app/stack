using Ocelot.Middleware;
using Ocelot.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("ocelot.json",optional: false, reloadOnChange: true)
    .AddJsonFile("Ocelot.auth.json", optional: false, reloadOnChange: true)
    .AddJsonFile("ocelot.inv.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

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
builder.Services.AddOcelot(builder.Configuration);

// TODO: Add Swagger

var app = builder.Build();

app.UseRouting();
app.UseCors(myAllowSpecificOrigins);
await app.UseOcelot();

app.Run();
