namespace notif_service.Models
{
    public class NotificationDtoV2
    {
        public string Title { get; set; } = null!;
        public string Message { get; set; } = null!;
        public ICollection<string> NotificationStrings { get; set; } = null!;
        public string? MailTo { get; set; } = null!;
        public int? channelId { get; set; }
        public int? workspaceId { get; set; }
        public Dictionary<string, string>? Links { get; set; } = new Dictionary<string, string>();
    }
}
