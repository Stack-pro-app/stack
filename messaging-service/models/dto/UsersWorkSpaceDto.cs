namespace messaging_service.models.dto
{
    public class UsersWorkSpaceDto
    {
        public int WorkspaceId { get; set; }
        public ICollection<int> UsersId { get; set; }
    }
}
