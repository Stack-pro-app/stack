using messaging_service.Models.Domain;
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
        public ICollection<UserWorkspace> UserWorkspaces { get; } = new List<UserWorkspace>();
        public ICollection<Invitation> Invitations { get; } = new List<Invitation>();
        public ICollection<Member> Memberships { get; } = new List<Member>();
        public ICollection<Chat> Messages { get; } = new List<Chat>();
        public ICollection<Workspace> WorkspacesAdmin { get; } = new List<Workspace>();
        public string AuthId { get; set; } = null!;
        public string NotificationString { get; set; } = null!;
        public string? ProfilePicture { get; set; }
        public string? ProfilePictureKey { get; set; }
        

    }
}
