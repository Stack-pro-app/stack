using AutoMapper;
using messaging_service.models.domain;
using messaging_service.models.dto.Requests;
using messaging_service.Repository;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace messaging_service.Consumer
{
    public class RabbitMQConsumer
    {
                private readonly string _queueName;
                private readonly ConnectionFactory _factory;
                private readonly IConnection _connection;
                private readonly IModel _channel;
                private readonly ChatRepository _chatRepository;
                private readonly IMapper _mapper;

                public RabbitMQConsumer(ChatRepository chatRepository,IMapper mapper)
                {
                    _queueName = "messages";
                    _factory = new ConnectionFactory() { HostName = "localhost" };
                    _connection = _factory.CreateConnection();
                    _channel = _connection.CreateModel();
                    _channel.QueueDeclare(queue: _queueName);
                    _chatRepository = chatRepository;
                     _mapper = mapper;
                 }

                public void StartConsuming()
                {
                    var consumer = new EventingBasicConsumer(_channel);
                    consumer.Received += async (model, ea) =>
                    {
                        try
                        {
                            var body = ea.Body.ToArray();
                            var messageString = Encoding.UTF8.GetString(body);

                            MessageRequestDto messageDto = JsonConvert.DeserializeObject<MessageRequestDto>(messageString);

                            Chat message = _mapper.Map<Chat>(messageDto);
                            var result = await _chatRepository.CreateChatAsync(message);
                            _channel.BasicAck(ea.DeliveryTag, false);
                        } catch (Exception ex)
                        {
                            var result = ex.Message;
                        }
                        
                    };
                    _channel.BasicConsume(queue: _queueName,
                                          autoAck: false,
                                          consumer: consumer);
                }
                public void CloseConnection()
                {
                    _connection.Close();
                    _channel.Close();
                }
    
    }

}
