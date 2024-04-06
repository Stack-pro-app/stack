namespace notif_service.Models
{
    public class NotificationDto
    {
        public string Id { get; set; } = null!;
        public string Message { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}
