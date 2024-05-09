

using notif_service.Models;

namespace notif_service.Services.Email
{
    public interface IEmailservice
    {
        public void SendEmailInvitation(EmailDto email);
    }
}
