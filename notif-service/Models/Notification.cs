using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace notif_service.Models
{
    public class Notification
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;
        public string Message { get; set; } = null!;
        public ICollection<NotificationString> NotificationStrings { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public int? channelId { get; set; }
        public int? workspaceId { get; set; }

    }

    public class NotificationString
    {
        public string Value { get; set; } = null!;
        public bool IsSeen { get; set; } = false;
    }
}
