using notif_service.Models;
using MongoDB.Driver;
using Microsoft.Extensions.Options;

namespace notif_service.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IMongoCollection<Notification> _notifications;

        public NotificationService(IOptions<NotificationDatabaseSettings> notificationOptions)
        {
            var mongoClient = new MongoClient(notificationOptions.Value.ConnectionString);
            var database = mongoClient.GetDatabase(notificationOptions.Value.DatabaseName);
            _notifications = database.GetCollection<Notification>(notificationOptions.Value.NotificationsCollectionName);
        }
        public async Task AddNotificationAsync(Notification notification) => await _notifications.InsertOneAsync(notification);

        public async Task<IEnumerable<Notification>> GetMoreNotificationsAsync(string userId, int page)
        {
            if (page < 0) throw new Exception("Invalid page");
            var filter = Builders<Notification>.Filter.Eq(n => n.UserId, userId) & Builders<Notification>.Filter.Eq(n => n.IsSeen, true);
            IEnumerable<Notification> notifications = await _notifications.Find(filter).SortByDescending(n=>n.CreatedAt).Skip((page-1)*10).Limit(10).ToListAsync();
            return notifications;
        }

        public async Task<IEnumerable<Notification>> GetUnseenNotificationsAsync(string userId)
        {
            var filter = Builders<Notification>.Filter.Eq(n => n.UserId, userId) & Builders<Notification>.Filter.Eq(n => n.IsSeen, false);
            IEnumerable<Notification> notifications = await _notifications.Find(filter).SortByDescending(n=>n.CreatedAt).ToListAsync();
            var update = Builders<Notification>.Update.Set(n => n.IsSeen, true);

            await _notifications.UpdateOneAsync(filter, update);

            return notifications;
        }

        public async Task SetNotificationsSeenAsync(string userId)
        {
            var filter = Builders<Notification>.Filter.Eq(n => n.UserId, userId) & Builders<Notification>.Filter.Eq(n => n.IsSeen, false);
            var update = Builders<Notification>.Update.Set(n => n.IsSeen, true);

            await _notifications.UpdateOneAsync(filter, update);
        }
    }
}
