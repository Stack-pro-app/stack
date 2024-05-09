using notif_service.Models;

namespace notif_service.Services
{
    public interface INotificationService
    {
        public Task<string> AddNotificationAsync(Notification notification);
        public Task SetNotificationsSeenAsync(string userId);
        public Task<List<Notification>> GetUnseenNotificationsAsync(string userId,int page);
        public Task<List<Notification>> GetMoreNotificationsAsync(string userId,int page);

    }
}
