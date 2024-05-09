using messaging_service.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace messaging_service.models.domain
{
    public class Workspace
    {
        public int Id { get; set; }
        [MaxLength(30)]
        public string Name { get; set; } = null!;
        public ICollection<Channel> Channels { get;} = new List<Channel>();
        public ICollection<UserWorkspace> UserWorkspaces { get; } = new List<UserWorkspace>();
        public ICollection<Invitation> Invitations { get;} = new List<Invitation>();
        //TODO: Send TO PM service workspace info with users authIds and admin authId
        public DateTime Created_at { get; set; }
        public int AdminId { get; set; }
        public User Admin { get; set; } = null!;
        
    }
}
