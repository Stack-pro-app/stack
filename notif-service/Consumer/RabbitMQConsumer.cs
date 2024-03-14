﻿using AutoMapper;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using notif_service.Services;
using notif_service.Models;
using Newtonsoft.Json;

namespace notif_service.Consumer
{
    public class RabbitMQConsumer : DefaultBasicConsumer
    {
        private string _queueName;
        private ConnectionFactory _factory;
        private IConnection _connection;
        private IModel _channel;
        private readonly IMapper _mapper;
        private readonly ILogger<RabbitMQConsumer> _logger;
        private readonly INotificationService _notificationService;
        private string hostName;
        private string userName;
        private string password;
        private string port;


        public RabbitMQConsumer( IMapper mapper, ILogger<RabbitMQConsumer> logger,INotificationService notificationService)
        {
            _mapper = mapper;
            _logger = logger;
            _notificationService = notificationService;
            hostName = Environment.GetEnvironmentVariable("MQ_HOST");
            userName = Environment.GetEnvironmentVariable("MQ_USER");
            password = Environment.GetEnvironmentVariable("MQ_PASSWORD");
            port = Environment.GetEnvironmentVariable("MQ_PORT");

        }

        public bool SetConnection()
        {
            _queueName = "notification";
            _factory = new ConnectionFactory()
            {
                //HostName = hostName,
                //UserName = userName,
                //Password = password,
                //Port = int.Parse(port),
                HostName = "localhost",
                //Port = 5672,
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
                return false;
            }


        }

        public async Task StartConsuming()
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                await HandleNotificationAsync(ea);
            };
            _channel.BasicConsume(queue: _queueName,
                                      autoAck: false,
                                      consumer: consumer);
        }

        private async Task HandleNotificationAsync(BasicDeliverEventArgs ea)
        {
            try
            {
                var body = ea.Body.ToArray();
                var messageString = Encoding.UTF8.GetString(body);

                NotificationDtoV2 notificationDto = JsonConvert.DeserializeObject<NotificationDtoV2>(messageString) ?? throw new Exception("Invalid Format");

                Notification notification = _mapper.Map<Notification>(messageString);

                await _notificationService.AddNotificationAsync(notification);

                _channel.BasicAck(ea.DeliveryTag, false);
            }
            catch (Exception ex)
            {
                var result = ex.Message;
                _logger.LogInformation("Result:" + result);
            }
        }

    }

}