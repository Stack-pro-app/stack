namespace notif_service.Models
{
    public class NotificationDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string NotificationsCollectionName { get; set; } = null!;
    }
}
