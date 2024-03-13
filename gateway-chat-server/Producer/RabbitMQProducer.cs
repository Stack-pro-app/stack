using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace gateway_chat_server.Producer
{
    public class RabbitMQProducer : IMessageProducer
    {
        public void SendMessage<T>(T message)
        {
            var factory = new ConnectionFactory {
                HostName = Environment.GetEnvironmentVariable("MQ_HOST"),
                UserName = Environment.GetEnvironmentVariable("MQ_USER"),
                Password = Environment.GetEnvironmentVariable("MQ_PASSWORD"),
                Port = int.Parse(Environment.GetEnvironmentVariable("MQ_PORT"))
            };
            var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare("message", durable: true, exclusive: false, autoDelete: false, arguments: null);
            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(exchange: "", routingKey: "message", body: body);
        }
        public void SendFile<T>(T message)
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                Port = 5672,
                UserName = "guest",
                Password = "guest"
                //HostName = Environment.GetEnvironmentVariable("MQ_HOST"),
                //UserName = Environment.GetEnvironmentVariable("MQ_USER"),
                //Password = Environment.GetEnvironmentVariable("MQ_PASSWORD"),
                //Port = int.Parse(Environment.GetEnvironmentVariable("MQ_PORT"))
            };
            var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare("file", durable: true, exclusive: false, autoDelete: false, arguments: null);
            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(exchange: "", routingKey: "file", body: body);
        }
    }
}
