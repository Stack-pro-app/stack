using notif_service.Models;
using MailKit.Net.Smtp;
using MimeKit;

namespace notif_service.Services
{
    public class EmailService:IEmailService
    {
        private readonly MailSettings _emailConfig;
        public EmailService(MailSettings emailConfig)
        {
            _emailConfig = emailConfig;
        }
        public async Task SendEmail(MailData message)
        {
            //var emailMessage = CreateEmailMessage(message);
           // await Send(emailMessage);
        }

        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Stack Team",_emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };

            return emailMessage;
        }

        private async Task Send(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, false);
                    await client.AuthenticateAsync(_emailConfig.Username, _emailConfig.Password);

                    await client.SendAsync(mailMessage);
                }
                catch
                {
                    //log an error message or throw an exception or both.
                    throw;
                }
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }
            }
        }
    }
}
