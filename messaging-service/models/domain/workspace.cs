using System.ComponentModel.DataAnnotations;

namespace messaging_service.models.domain
{
    public class Workspace
    {
        public int Id { get; set; }
        [MaxLength(30)]
        public string Name { get; set; }
        public ICollection<Channel> Channels { get; set; } = new List<Channel>();
        public ICollection<UserWorkspace> UserWorkspaces { get; set; } = new List<UserWorkspace>();
        public DateTime Created_at { get; set; }
        public int AdminId { get; set; }
    }
}
