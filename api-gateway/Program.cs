using Ocelot.Middleware;
using Ocelot.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("ocelot.json",optional: false, reloadOnChange: true)
    .AddJsonFile("Ocelot.auth.json", optional: false, reloadOnChange: true)
    .AddJsonFile("ocelot.inv.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.AddSignalR();
builder.Services.AddOcelot(builder.Configuration);

// TODO: Add Swagger

var app = builder.Build();

app.UseRouting();
await app.UseOcelot();

app.Run();
