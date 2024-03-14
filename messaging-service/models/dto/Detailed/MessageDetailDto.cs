using messaging_service.models.dto.Minimal;

namespace messaging_service.models.dto.Detailed
{
    public class MessageDetailDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public UserMinimalDto User { get; set; }
        public int ChannelId { get; set; }
        public string Message { get; set; }
        public DateTime? Modified_at { get; set; }
        public DateTime Created_at { get; set; }
        public int ParentId { get; set; }
        public MessageMinimalDto? Parent { get; set; }
        public string? Attachement_Url { get; set; }
        public string? Attachement_Name { get; set; }
        public string? Attachement_Key { get; set; }
    }
}
