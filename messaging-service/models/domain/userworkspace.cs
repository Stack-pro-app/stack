namespace messaging_service.models.domain
{
    public class UserWorkspace
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int WorkspaceId { get; set; }
        public User User { get; set; }
        public Workspace Workspace { get; set; }
        public DateTime Created_at { get; set; }

    }
}
