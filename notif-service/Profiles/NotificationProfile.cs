using AutoMapper;
using notif_service.Models;

namespace notif_service.Profiles
{
    public class NotificationProfile:Profile
    {
        public NotificationProfile() { 
            CreateMap<NotificationDto, Notification>();
            CreateMap<Notification, NotificationDto>();
        }
    }
}
