using notif_service.Producer;
using notif_service.Models;

namespace notif_service.Services.Email
{
    public class EmailService : IEmailservice
    {
        IRabbitMQProducer _rabbitMQProducer;
        public EmailService(IRabbitMQProducer producer) { 
            _rabbitMQProducer = producer;
        }
        public void SendEmailInvitation(EmailDto email)
        {
            _rabbitMQProducer.SendToQueue(email,"email");
        }
    }
}
