using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace messaging_service.models
{
    public class Channel
    {
        public int Id { get; set; }
        [MaxLength(30)]
        public string Name { get; set; }
        [MaxLength(300)]
        public string? Description { get; set; }

        public DateTime Created_at { get; set; }
        public bool Is_private { get; set; } = true;
        public int WorkspaceId { get; set; }
        public Workspace Workspace { get; set; } = null!;
        public ICollection<Member> Members { get; set; }= new List<Member>();
        public ICollection<Chat> Channels { get; set; }= new List<Chat>();
    }
}
