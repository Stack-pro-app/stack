using notif_service.Models;

namespace notif_service.Services
{
    public interface INotificationService
    {
        public Task AddNotificationAsync(Notification notification);
        public Task SetNotificationsSeenAsync(string userId);
        public Task<IEnumerable<Notification>> GetUnseenNotificationsAsync(string userId);
        public Task<IEnumerable<Notification>> GetMoreNotificationsAsync(string userId,int page);

    }
}
