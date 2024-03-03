using System.ComponentModel.DataAnnotations;

namespace messaging_service.models.domain
{
    public class Chat
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int ChannelId { get; set; }
        public Channel Channel { get; set; }
        [MaxLength(500)]
        public string Message { get; set; }
        public bool Is_deleted { get; set; } = false;
        public DateTime? Modified_at { get; set; }
        public DateTime Created_at { get; set; }
        public int? ParentId { get; set; }
        public Chat? Parent { get; set; }
        public ICollection<int>? TaggedIds { get; set; } = new List<int>();
    }
}
