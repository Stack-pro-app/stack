using Ocelot.Middleware;
using Ocelot.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
var myAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:4200")
                          .AllowCredentials()
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                      });
});
builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
//    .AddJsonFile("ocelot.json",optional: false, reloadOnChange: true)
    .AddJsonFile("ocelotDEV.json",optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.AddSignalR();
builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();
app.UseCors(myAllowSpecificOrigins);

app.UseRouting();
await app.UseOcelot();


app.Run();
