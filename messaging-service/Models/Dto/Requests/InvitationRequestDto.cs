using messaging_service.models.domain;

namespace messaging_service.Models.Dto.Requests
{
    public class InvitationRequestDto
    {
        public int WorkspaceId { get; set; }
        public int UserId { get; set; }
    }

    public class InvitationRequestDtoV2
    {
        public int WorkspaceId { get; set; }
        public string AuthId { get; set; } = null!;
    }
}
