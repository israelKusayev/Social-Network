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
        private IDynamoDbRepository<Notification> _notificationsRepository;

        public NotificationsManager(IConfiguration configuration)
        {
            _notificationsRepository = new DynamoDbRepository<Notification>(configuration);
        }

        public void SendNotification<T>(IHubContext<T> hub, string UserId, string methood, object pyload) where T : Hub
        {
            hub.Clients.User(UserId).SendAsync(methood, pyload);
            AddToHistory(UserId, methood, pyload);
        }

        public void AddToHistory(string UserId, string methood, object pyload)
        {
            long timeStamp = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
            Notification item = new Notification()
            {
                UserId = UserId,
                TimeStamp = timeStamp,
                DataJson = JsonConvert.SerializeObject(pyload)
            };
            _notificationsRepository.Add(item);
        }

        public List<Notification> GetNotifications(string userId)
        {
            return _notificationsRepository.GetAll(userId);
        }
    }
}
