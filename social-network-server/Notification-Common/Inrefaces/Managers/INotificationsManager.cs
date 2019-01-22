using System.Collections.Generic;
using Microsoft.AspNetCore.SignalR;

namespace Notification_Common.Interfaces.Managers
{
    public interface INotificationsManager
    {
        void AddToHistory(string UserId, string methood, object pyload);
        List<string> GetNotifications(string userId);
        void SendNotification<T>(IHubContext<T> hub, string UserId, string methood, object pyload) where T : Hub;
    }
}