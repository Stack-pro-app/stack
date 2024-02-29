using System.ComponentModel.DataAnnotations;

namespace messaging_service.models.domain
{
    public class Workspace
    {
        public int Id { get; set; }
        [MaxLength(30)]
        public string Name { get; set; }
        public ICollection<Channel> Channels { get; set; } = new List<Channel>();
        public ICollection<User> Users { get; set; } = new List<User>();
        public DateTime Created_at { get; set; }
    }
}
