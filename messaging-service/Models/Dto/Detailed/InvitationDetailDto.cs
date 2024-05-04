using messaging_service.models.domain;

namespace messaging_service.Models.Dto.Detailed
{
    public class InvitationDetailDto
    {
        public int Id { get; set; }
        public string Token { get; set; } = null!;

        public int WorkspaceId { get; set; }
        public int UserId { get; set; }
    }
}
