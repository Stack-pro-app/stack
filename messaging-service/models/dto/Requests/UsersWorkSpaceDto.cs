namespace messaging_service.models.dto.Requests
{
    public class UsersWorkSpaceDto
    {
        public int WorkspaceId { get; set; }
        public ICollection<int>? UsersId { get; set; }
        public ICollection<string>? Emails { get; set; }
    }
}
