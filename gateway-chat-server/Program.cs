using Amazon.S3;
using gateway_chat_server.Hubs;
using gateway_chat_server.Producer;
var myAllowSpecificOrigins = "_myAllowSpecificOrigins";

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
    options.AddPolicy(name: myAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:4200") // Change port to 4200
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});



builder.Services.AddSingleton<IMessageProducer, RabbitMQProducer>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

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
app.UseCors(myAllowSpecificOrigins);


app.Run();
