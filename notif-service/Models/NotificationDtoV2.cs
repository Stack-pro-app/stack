namespace notif_service.Models
{
    public class NotificationDtoV2
    {
        public string Message { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public string MailTo { get; set; } = null!;
    }
}
