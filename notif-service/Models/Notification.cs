using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace notif_service.Models
{
    public class Notification
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Message { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public bool IsSeen { get; set; }= false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

    }
}
