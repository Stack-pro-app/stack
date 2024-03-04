using AutoMapper;
using messaging_service.models.domain;
using messaging_service.models.dto.Detailed;
using messaging_service.models.dto.Minimal;
using messaging_service.models.dto.Requests;

namespace messaging_service.MappingProfiles
{
    public class ChannelProfile: Profile
    {
        public ChannelProfile() {
            CreateMap<Channel, ChannelRequestDto>();
            CreateMap<ChannelRequestDto, Channel>();
            CreateMap<Channel, ChannelUpdateDto>();
            CreateMap<ChannelUpdateDto, Channel>();
            CreateMap<ChannelResponseDto, Channel>();
            CreateMap<Channel, ChannelResponseDto>();
            CreateMap<Channel, ChannelMinimalDto>();
            CreateMap<ChannelMinimalDto,Channel>();
            CreateMap<ChannelDetailDto,Channel>();
            CreateMap<Channel,ChannelDetailDto>();
        }
    }
}
