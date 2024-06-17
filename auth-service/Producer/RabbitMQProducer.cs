using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;


namespace auth_service.Producer;


public class RabbitMQProducer : IRabbitMQProducer
{
    public void SendRegistration<T>(T register,string queue)
    {

        var factory = new ConnectionFactory
        {
            HostName = Environment.GetEnvironmentVariable("MQ_HOST") ?? "rabbitmq.stack-app.test",
            UserName = Environment.GetEnvironmentVariable("MQ_USER") ?? "user",
            Password = Environment.GetEnvironmentVariable("MQ_PASSWORD") ?? "password",
            Port = int.Parse(Environment.GetEnvironmentVariable("MQ_PORT") ?? "5672")
        };
        
        var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
        channel.QueueDeclare(queue, durable: true, exclusive: false, autoDelete: false, arguments: null);
        var json = JsonConvert.SerializeObject(register);
        var body = Encoding.UTF8.GetBytes(json);
        
        
        channel.BasicPublish(exchange: "", routingKey:queue , body:body);

    }
}
