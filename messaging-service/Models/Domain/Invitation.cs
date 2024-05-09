using messaging_service.models.domain;

namespace messaging_service.Models.Domain
{
    public class Invitation
    {
        public int Id { get; set; }
        public string Token { get; set; } = null!;
        public int WorkspaceId { get; set; }
        public Workspace Workspace { get; set; } = null!;
        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
