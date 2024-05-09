using AutoMapper;
using messaging_service.Models.Domain;
using messaging_service.Models.Dto.Detailed;
using messaging_service.Models.Dto.Requests;

namespace messaging_service.MappingProfiles
{
    public class InvitationProfile:Profile
    {
        public InvitationProfile()
        {
            CreateMap<InvitationRequestDto, Invitation>();
            CreateMap<Invitation, InvitationRequestDto>();
            CreateMap<Invitation, InvitationRequestDtoV2>();
            CreateMap<InvitationRequestDtoV2, Invitation>();
            CreateMap<InvitationDetailDto, Invitation>();
            CreateMap<Invitation,InvitationDetailDto>();
        }
    }
}
