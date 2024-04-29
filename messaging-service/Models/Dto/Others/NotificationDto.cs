namespace messaging_service.Models.Dto.Others
{
    public class NotificationDto
    {
        public string Message { get; set; } = null!;
        public ICollection<string> NotificationStrings { get; set; } = null!;
        public string? MailTo { get; set; } = null!;
        public int? channelId { get; set; }
        public int? workspaceId { get; set; }
    }
}
