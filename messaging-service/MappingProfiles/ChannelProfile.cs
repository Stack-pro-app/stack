using AutoMapper;
using messaging_service.models.dto;
using messaging_service.models.dto.Response;
using messaging_service.models.domain;

namespace messaging_service.MappingProfiles
{
    public class ChannelProfile: Profile
    {
        public ChannelProfile() {
            CreateMap<Channel, ChannelDto>();
            CreateMap<ChannelDto, Channel>();
            CreateMap<Channel, ChannelUpdateDto>();
            CreateMap<ChannelUpdateDto, Channel>();
            CreateMap<ChannelResponseDto, Channel>();
            CreateMap<Channel, ChannelResponseDto>();
        }
    }
}
