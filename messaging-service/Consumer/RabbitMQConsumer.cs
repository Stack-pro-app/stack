using AutoMapper;
using messaging_service.Data;
using messaging_service.models.domain;
using messaging_service.models.dto.Requests;
using messaging_service.Models.Dto.Others;
using messaging_service.Producer;
using messaging_service.Repository;
using messaging_service.Repository.Interfaces;
using messaging_service.Services;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace messaging_service.Consumer
{
    public class RabbitMQConsumer : DefaultBasicConsumer
    {
        private string? _queueName;
        private string? _queueName2;
        private ConnectionFactory? _factory;
        private IConnection? _connection;
        private IModel? _channel;
        private IModel? _channel2;
        private readonly IChatRepository _chatRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<RabbitMQConsumer> _logger;
        private string hostName;
        private string userName;
        private string password;
        private string port;


        public RabbitMQConsumer(IChatRepository chatRepository, IMapper mapper, ILogger<RabbitMQConsumer> logger,INotificationService notif,IUserRepository ur)
        {
            _chatRepository = chatRepository;
            _userRepository = ur;
            _mapper = mapper;
            _logger = logger;
            hostName = Environment.GetEnvironmentVariable("MQ_HOST") ?? "localhost";
            //hostName = "localhost";
            userName = Environment.GetEnvironmentVariable("MQ_USER") ?? "guest";
            password = Environment.GetEnvironmentVariable("MQ_PASSWORD") ?? "guest";
            port = Environment.GetEnvironmentVariable("MQ_PORT") ?? "5672";
            //port = "5672";

        }

        public bool SetConnection()
        {
            _queueName = "message";
            _queueName2 = "register-msg";
            _factory = new ConnectionFactory()
            {
                HostName = hostName,
                UserName = userName,
                Password = password,
                Port = int.Parse(port),
                DispatchConsumersAsync = true
            };
            try
            {
                _connection = _factory.CreateConnection();
                _channel = _connection.CreateModel();
                _channel.QueueDeclare(queue: _queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
                _channel2 = _connection.CreateModel();
                _channel2.QueueDeclare(queue: _queueName2, durable: true, exclusive: false, autoDelete: false, arguments: null);
                return true;
            }
            catch (Exception)
            {
                Task.Delay(TimeSpan.FromSeconds(1));
                return false;
            }
        }
        public void StartConsuming()
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);
            var consumer2 = new AsyncEventingBasicConsumer(_channel2);
            consumer.Received += async (model, ea) =>
            {
                await HandleMessageAsync(ea);
            };
            consumer2.Received += async (model, ea) =>
            {
                await HandleRegistrationAsync(ea);
            };
            _channel.BasicConsume(queue: _queueName,
                                      autoAck: false,
                                      consumer: consumer);
            _channel2.BasicConsume(queue: _queueName2,
                                      autoAck: false,
                                      consumer: consumer2);
        }

        private async Task HandleMessageAsync(BasicDeliverEventArgs ea)
        {
            try
            {
                var body = ea.Body.ToArray();
                var messageString = Encoding.UTF8.GetString(body);
                _logger.LogInformation(messageString);

                MessageRequestDto messageDto = JsonConvert.DeserializeObject<MessageRequestDto>(messageString) ?? throw new ValidationException("Failed to deserialize Message");

                if (messageDto.Message.IsNullOrEmpty() && messageDto.Attachement_Url.IsNullOrEmpty()) throw new ValidationException("Empty Message");

                Chat message = _mapper.Map<Chat>(messageDto);
                await _chatRepository.CreateChatAsync(message);


                _channel?.BasicAck(ea.DeliveryTag, false);
            }
            catch (Exception ex)
            {
                var result = ex.Message;
                _logger.LogInformation("Result:" + result);
            }
        }

        private async Task HandleRegistrationAsync(BasicDeliverEventArgs ea)
        {
            try
            {
                var body = ea.Body.ToArray();
                var registrationString = Encoding.UTF8.GetString(body);
                _logger.LogInformation(registrationString);

                RegisterDto registerDto = JsonConvert.DeserializeObject<RegisterDto>(registrationString) ?? throw new ValidationException("Failed to deserialize Registration");

                if (registerDto.Email.IsNullOrEmpty() || registerDto.ID.IsNullOrEmpty() || registerDto.Name.IsNullOrEmpty()) throw new ValidationException("Empty Register Info");

                User user = new()
                {
                    AuthId = registerDto.ID,
                    Email = registerDto.Email,
                    Name = registerDto.Name
                };
                await _userRepository.CreateUserAsync(user);


                _channel?.BasicAck(ea.DeliveryTag, false);
            }
            catch (Exception ex)
            {
                var result = ex.Message;
                _logger.LogInformation("Result:" + result);
            }
        }

    }

}
