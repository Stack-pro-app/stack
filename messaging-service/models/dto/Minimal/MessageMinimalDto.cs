namespace messaging_service.models.dto.Minimal
{
    public class MessageMinimalDto
    {
        public int UserId { get; set; }
        public Guid MessageId { get; set; }
        public UserMinimalDto User { get; set; }
        public int ChannelId { get; set; }
        public string Message { get; set; }
        public string? Attachement_Name { get; set; }
    }
}
