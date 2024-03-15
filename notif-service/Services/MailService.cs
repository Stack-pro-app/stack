using Amazon.SimpleEmail.Model;
using Amazon.SimpleEmail;
using AutoMapper.Internal;
using Microsoft.Extensions.Options;
using MailKit.Security;
using MimeKit;
using notif_service.Models;
using MailKit.Net.Smtp;

namespace notif_service.Services
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;
        private readonly IAmazonSimpleEmailService _mailService;
        public MailService(IOptions<MailSettings> mailSettings,
            IAmazonSimpleEmailService mailService)
        {
            _mailSettings = mailSettings.Value;
            _mailService = mailService;
        }
        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            var mailBody = new Body(new Content(mailRequest.Body));
            var message = new Message(new Content(mailRequest.Subject), mailBody);
            var destination = new Destination(new List<string> { mailRequest.ToEmail! });
            var request = new SendEmailRequest(_mailSettings.Mail, destination, message);
            await _mailService.SendEmailAsync(request);
        }
    }
}

