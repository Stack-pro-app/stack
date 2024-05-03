using messaging_service.models.domain;

namespace messaging_service.Models.Dto.Detailed
{
    public class InvitationDetailDto
    {
        public string Id { get; set; } = null!; // This is Token
        public int WorkspaceId { get; set; }
        public int UserId { get; set; }
        public bool IsAccepted { get; set; } = false;
    }
}
