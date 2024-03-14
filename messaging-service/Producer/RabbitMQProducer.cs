using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace messaging_service.Producer
{
    public class RabbitMQProducer : IRabbitMQProducer
    {
        public void SendNotification<T>(T notification)
        {
                var factory = new ConnectionFactory
                {
                    HostName = "localhost",
                    //HostName = Environment.GetEnvironmentVariable("MQ_HOST"),
                    //UserName = Environment.GetEnvironmentVariable("MQ_USER"),
                    //Password = Environment.GetEnvironmentVariable("MQ_PASSWORD"),
                    //Port = int.Parse(Environment.GetEnvironmentVariable("MQ_PORT"))
                };
                var connection = factory.CreateConnection();
                using var channel = connection.CreateModel();
                channel.QueueDeclare("notification", durable: true, exclusive: false, autoDelete: false, arguments: null);
                var json = JsonConvert.SerializeObject(notification);
                var body = Encoding.UTF8.GetBytes(json);

                channel.BasicPublish(exchange: "", routingKey: "notification", body: body);
        }
    }
}
