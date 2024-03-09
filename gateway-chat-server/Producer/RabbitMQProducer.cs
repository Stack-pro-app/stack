using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace gateway_chat_server.Producer
{
    public class RabbitMQProducer : IMessageProducer
    {
        private readonly ILogger _logger;

        public RabbitMQProducer(ILogger<RabbitMQProducer> logger) { _logger = logger; }
        public void SendMessage<T>(T message)
        {
            _logger.LogInformation("Send Message Excuted");
            var factory = new ConnectionFactory {
                HostName = "rabbitmq",
                UserName = "user",
                Password = "password",
                Port = 5672
            };
            var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare("message", durable: true, exclusive: false, autoDelete: false, arguments: null);
            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);
            _logger.LogInformation("Json Message {json}",json);

            channel.BasicPublish(exchange: "", routingKey: "message", body: body);
        }
    }
}
