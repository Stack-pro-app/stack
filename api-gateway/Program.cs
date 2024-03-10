using Ocelot.Middleware;
using Ocelot.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("Ocelot.json",optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.AddSignalR();
builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();
app.UseRouting();
app.UseOcelot();


app.Run();
