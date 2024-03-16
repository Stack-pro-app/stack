using notif_service.Models;

namespace notif_service.Services
{
    public interface IEmailService
    {
        Task SendEmail(Message message);
        
    }
}
