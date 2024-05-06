using messaging_service.Models.Dto.Others;

namespace messaging_service.Services
{
    public interface INotificationService
    {
        public Task SendMessageNotif(int channelId);
        public Task SendJoiningChannelNotif(int channelId);
        public Task SendJoiningWorkspaceNotif(int workspaceId);
        public void SendNotification(NotificationDto notif);
    }
}
