using AutoMapper;
using messaging_service.models.domain;
using messaging_service.models.dto;

namespace messaging_service.MappingProfiles
{
    public class MemberProfile : Profile
    {
        public MemberProfile() {
            CreateMap<Member, MemberDto>();
        }
    }
}
