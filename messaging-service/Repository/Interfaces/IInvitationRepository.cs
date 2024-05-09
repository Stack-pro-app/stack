using messaging_service.Models.Domain;

namespace messaging_service.Repository.Interfaces
{
    public interface IInvitationRepository
    {
        public Task<Invitation> FindInvitationByToken(string token);
        public Task CreateInvitation(Invitation inv);
        public Task CreateInvitation(int workspaceId, string authId);
        public Task AcceptInvitation(string token);
        public Task DeleteInvitation(string token); // Decline = delete
        public Task<IEnumerable<Invitation>> GetInvitations(int userId);
        public Task<IEnumerable<Invitation>> GetInvitations(string authId);


    }
}
