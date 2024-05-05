using System.ComponentModel.DataAnnotations;

namespace messaging_service.models.domain
{
    public class Chat
    {
        public int Id { get; set; }
        public Guid MessageId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public int ChannelId { get; set; }
        public Channel Channel { get; set; }= null!;
        [MaxLength(500)]
        public string? Message { get; set; }
        public bool Is_deleted { get; set; } = false;
        public DateTime? Modified_at { get; set; }
        public DateTime Created_at { get; set; }
        public int? ParentId { get; set; }
        public Chat? Parent { get; set; }
        public ICollection<Chat>? Children { get; set; }
        public string? Attachement_Url { get; set; }
        public string? Attachement_Name { get; set; }
        public string? Attachement_Key { get; set; }
    }
}
