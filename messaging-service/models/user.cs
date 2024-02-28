namespace messaging_service.models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Last_login { get; set;}
        public int WorkspaceId { get; set; }
        public Workspace Workspace { get; set; }
        public ICollection<Member> Memberships { get; set; } = new List<Member>();
    }
}
