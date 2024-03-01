using AutoMapper;
using messaging_service.models.domain;
using messaging_service.models.dto;
using messaging_service.models.dto.Response;

namespace messaging_service.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile() {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<User, UserResponseDto>();
        }
    }
}
