using AutoMapper;
using messaging_service.models.domain;
using messaging_service.models.dto.Detailed;
using messaging_service.models.dto.Minimal;
using messaging_service.models.dto.Requests;

namespace messaging_service.MappingProfiles
{
    public class ChatProfile : Profile
    {
        public ChatProfile() {
            CreateMap<Chat, MessageMinimalDto>();
            CreateMap<MessageMinimalDto,Chat> ();
            CreateMap<Chat, MessageDetailDto>()
                .ForMember(dest => dest.User, act => act.MapFrom(src=> src.User))
                .ForMember(dest => dest.Parent, act => act.MapFrom(src => src.Parent));
            CreateMap<MessageDetailDto, Chat>()
            .ForMember(dest => dest.User, act => act.MapFrom(src => src.User))
            .ForMember(dest => dest.Parent, act => act.MapFrom(src => src.Parent));
            CreateMap<MessageRequestDto, Chat>();
        }
    }
}
