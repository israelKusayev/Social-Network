using Microsoft.AspNetCore.SignalR;
using Notification_Common.Interfaces.Repositories;
using Notification_Common.Models;
using Notification_Dal;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Notification_Bl.Managers
{
    public class NotificationsManager
    {
        private IDynamoDbRepository<NotificationKey> _notificationKeysReopsitory;
        private IDynamoDbRepository<Notification> _notificationsRepository;

        public NotificationsManager(IConfiguration configuration)
        {
            _notificationKeysReopsitory = new DynamoDbRepository<NotificationKey>(configuration);
            //_notificationsRepository = new DynamoDbRepository<Notification>();
        }

        public string GetConectionId(string userId)
        {
            var keyRecord = _notificationKeysReopsitory.Get(userId);
            return keyRecord?.ConnectionId;
        }

        public bool AddConectionId(string userId, string connectionId)
        {
            NotificationKey item = new NotificationKey()
            {
                UserId = userId,
                ConnectionId = connectionId
            };
            if (!_notificationKeysReopsitory.Update(item))
            {
                if (!_notificationKeysReopsitory.Add(item))
                    return false;
            }
            return true;
        }

        public void SendNotification<T>(IHubContext<T> hub, string UserId, string methood, object pyload) where T : Hub
        {
            //string conectionId = GetConectionId(UserId);
            //hub.Clients.Client(conectionId).SendAsync(methood, pyload);
            hub.Clients.User(UserId).SendAsync(methood, pyload);
            //AddToHistory(UserId, methood, pyload);
        }

        public void AddToHistory(string UserId, string methood, object pyload)
        {
            long timeStamp = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
            Notification item = new Notification()
            {
                UserId = UserId,
                TimeStamp = timeStamp,
                DataJson = JsonConvert.SerializeObject(pyload),
                Methood = methood
            };
            _notificationsRepository.Add(item);
        }
    }
}
