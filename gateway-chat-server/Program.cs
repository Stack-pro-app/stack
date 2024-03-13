using Amazon.S3;
using gateway_chat_server.Hubs;
using gateway_chat_server.Producer;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());
builder.Services.AddAWSService<IAmazonS3>();
// Add services to the container.
builder.Services.AddSignalR().AddJsonProtocol();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllOrigins",
        builder =>
        {
            builder
                .WithOrigins("http://localhost", "https://localhost","null") // Allow requests from "null" origin
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials(); // Allow credentials if needed (e.g., for SignalR)
        });
});
/*
builder.Services.AddCors(options =>
{
    options.AddPolicy(name:MyAllowSpecificOrigins,
        policy =>
        {
            policy
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
                
        });
});
*/
builder.Services.AddSingleton<IMessageProducer, RabbitMQProducer>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllOrigins");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseRouting();
app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();
app.MapHub<ChannelHub>("/channelHub");
app.MapHub<FileHub>("/fileHub");


app.Run();
