using AutoMapper;
using messaging_service.models.domain;
using messaging_service.models.dto.Detailed;
using messaging_service.models.dto.Minimal;

namespace messaging_service.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile() {
            CreateMap<User, UserMinimalDto>();
            CreateMap<UserMinimalDto, User>();
            CreateMap<User, UserDetailDto>();
            CreateMap<UserDetailDto, User>();
        }
    }
}
