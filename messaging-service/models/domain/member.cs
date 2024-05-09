using System.ComponentModel.DataAnnotations.Schema;

namespace messaging_service.models.domain
{
    public class Member
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public int ChannelId { get; set; }
        public Channel Channel { get; set; } = null!;
        public DateTime Created_at { get; set; }
    }
}
