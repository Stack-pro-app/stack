using AutoMapper;
using notif_service.Models;

namespace notif_service.Profiles
{
    public class NotificationProfile:Profile
    {
        public NotificationProfile() {
            CreateMap<Notification, NotificationDtoV2>();
            CreateMap<NotificationDtoV2, Notification>()
            .ForMember(dest => dest.NotificationStrings, opt => opt.MapFrom(src => src.NotificationStrings.Select(s => new NotificationString { Value = s, IsSeen = false })));
            CreateMap<NotificationDto, Notification>();
            CreateMap<Notification, NotificationDto>();
            CreateMap<NotificationDtoV2, EmailDto>();
            CreateMap<EmailDto, NotificationDtoV2>();

        }
    }
}
