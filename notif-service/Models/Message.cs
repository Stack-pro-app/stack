using MailKit.Net.Smtp;
using System.Linq;
using System.Collections.Generic;
using MimeKit;

namespace notif_service.Models
{
    public class Message
    {
        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public Message(IEnumerable<string> to, string subject, string content)
        {
            To = new List<MailboxAddress>();

            To.AddRange(to.Select(x => new MailboxAddress("Stack Team",x)));
            Subject = subject;
            Content = content;
        }
    }
}
