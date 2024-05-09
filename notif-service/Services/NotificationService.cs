using notif_service.Models;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using MongoDB.Bson;

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
        public async Task<string> AddNotificationAsync(Notification notification)
        {
            await _notifications.InsertOneAsync(notification);

            return notification.ToJson();
        }

        public async Task<List<Notification>> GetMoreNotificationsAsync(string notificationString, int page)
        {
            if (page < 0) throw new Exception("Invalid page");
            var filter = Builders<Notification>.Filter.ElemMatch(n => n.NotificationStrings, ns => ns.Value == notificationString && ns.IsSeen == true);
            var notifications = await _notifications.Find(filter).SortByDescending(n => n.CreatedAt).Skip((page - 1) * 10).Limit(10).ToListAsync();
            return notifications;
        }

        public async Task<List<Notification>> GetUnseenNotificationsAsync(string notificationString, int page)
        {
            var filter = Builders<Notification>.Filter.ElemMatch(n => n.NotificationStrings, ns => ns.Value == notificationString && ns.IsSeen == false);
            var notifications = await _notifications.Find(filter)
                                                .SortByDescending(n => n.CreatedAt)
                                                .Skip((page - 1) * 10)
                                                .Limit(10)
                                                .ToListAsync();

            var notificationIds = notifications.Select(n => n.Id).ToList();
            var updateFilter = Builders<Notification>.Filter.And(
                filter,
                Builders<Notification>.Filter.In(n => n.Id, notificationIds)
            );

            var update = Builders<Notification>.Update.Set("NotificationStrings.$.IsSeen", true);
            await _notifications.UpdateManyAsync(updateFilter, update);

            return notifications;

        }

        public async Task SetNotificationsSeenAsync(string notificationString)
        {
            var filter = Builders<Notification>.Filter.ElemMatch(n => n.NotificationStrings, ns => ns.Value == notificationString && ns.IsSeen == false);
            var update = Builders<Notification>.Update.Set("NotificationStrings.$.IsSeen", true);

            await _notifications.UpdateOneAsync(filter, update);
        }
    }
}
