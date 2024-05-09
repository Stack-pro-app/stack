using messaging_service.Models.Domain;
using messaging_service.Models.Dto.Requests;

namespace messaging_service.Services
{
    public interface IInvitationService
    {
        public Task SendInvitation(InvitationRequestDto inv);
        public Task AcceptInvistation(string token);
        public Task DeclineInvitation(string token);
        public Task<IEnumerable<Invitation>> GetAllInvitations(int userId);
    }
}
