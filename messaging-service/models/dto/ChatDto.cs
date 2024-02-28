using System.ComponentModel.DataAnnotations;

namespace messaging_service.models.dto
{
    public class ChatDto
    {
            public int Id { get; set; }
            public int UserId { get; set; }
            public int ChannelId { get; set; }
            public string Message { get; set; }
            public bool Is_deleted { get; set; }
            public DateTime? Modified_at { get; set; }
            public DateTime Created_at { get; set; }
            public int? ParentId { get; set; }
            public ICollection<int>? TaggedIds { get; set; }
    }
}
