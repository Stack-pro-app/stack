using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;


namespace auth_service.Producer;


public class RabbitMQProducer : IRabbitMQProducer
{
    public void SendRegistration<T>(T register)
    {

        var factory = new ConnectionFactory
        {
            HostName = Environment.GetEnvironmentVariable("MQ_HOST") ?? "localhost",
            UserName = Environment.GetEnvironmentVariable("MQ_USER") ?? "guest",
            Password = Environment.GetEnvironmentVariable("MQ_PASSWORD") ?? "guest",
            Port = int.Parse(Environment.GetEnvironmentVariable("MQ_PORT") ?? "5672")
        };
        
        var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
        channel.QueueDeclare("registration", durable: true, exclusive: false, autoDelete: false, arguments: null);
        var json = JsonConvert.SerializeObject(register);
        var body = Encoding.UTF8.GetBytes(json);
        
        
        channel.BasicPublish(exchange: "", routingKey:"registration" , body:body);

    }
}