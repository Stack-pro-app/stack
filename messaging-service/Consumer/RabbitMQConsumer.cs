﻿using AutoMapper;
using messaging_service.Data;
using messaging_service.models.domain;
using messaging_service.models.dto.Requests;
using messaging_service.Models.Dto.Others;
using messaging_service.Producer;
using messaging_service.Repository;
using messaging_service.Repository.Interfaces;
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
        private ConnectionFactory? _factory;
        private IConnection? _connection;
        private IModel? _channel;
        private readonly IChatRepository _chatRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<RabbitMQConsumer> _logger;
        private string hostName;
        private string userName;
        private string password;
        private string port;
        private readonly IRabbitMQProducer _producer;


        public RabbitMQConsumer(IChatRepository chatRepository, IMapper mapper, ILogger<RabbitMQConsumer> logger,IRabbitMQProducer producer)
        {
            _chatRepository = chatRepository;
            _mapper = mapper;
            _logger = logger;
            _producer = producer;
            hostName = Environment.GetEnvironmentVariable("MQ_HOST") ?? "localhost";
            userName = Environment.GetEnvironmentVariable("MQ_USER") ?? "guest";
            password = Environment.GetEnvironmentVariable("MQ_PASSWORD") ?? "guest";
            port = Environment.GetEnvironmentVariable("MQ_PORT") ?? "5672";

        }

        public bool SetConnection()
        {
            _queueName = "message";
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
            consumer.Received += async (model, ea) =>
            {
                await HandleMessageAsync(ea);
            };
            _channel.BasicConsume(queue: _queueName,
                                      autoAck: false,
                                      consumer: consumer);
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

                NotificationDto notification = new()
                {
                    Message = "New Message",
                    NotificationStrings = new List<string>() { "string1", "string2", "string4" },

                };
                _producer.SendNotification(notification);


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
