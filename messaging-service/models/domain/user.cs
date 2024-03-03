using System.ComponentModel.DataAnnotations;

namespace messaging_service.models.domain
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime? Last_login { get; set; }
        public ICollection<UserWorkspace>? UserWorkspaces { get; set; } = new List<UserWorkspace>();
        public ICollection<Member>? Memberships { get; set; } = new List<Member>();
        public ICollection<Chat>? Messages { get; set; } = new List<Chat>();
        public int AuthId { get; set; }
    }
}
