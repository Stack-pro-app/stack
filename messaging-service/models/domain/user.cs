using System.ComponentModel.DataAnnotations;

namespace messaging_service.models.domain
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        [EmailAddress]
        public string Email { get; set; } = null!;
        public DateTime Created_at { get; set; }
        public DateTime? Last_login { get; set; }
        public ICollection<UserWorkspace>? UserWorkspaces { get; set; } = new List<UserWorkspace>();
        public ICollection<Member>? Memberships { get; set; } = new List<Member>();
        public ICollection<Chat>? Messages { get; set; } = new List<Chat>();
        public string AuthId { get; set; } = null!;
        public string NotificationString { get; set; } = null!;
        

    }
}
