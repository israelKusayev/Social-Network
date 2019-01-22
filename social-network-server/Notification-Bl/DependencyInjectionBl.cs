using Microsoft.Extensions.DependencyInjection;
using Notification_Bl.Managers;
using Notification_Common.Interfaces.Managers;
using Notification_Common.Interfaces.Repositories;
using Notification_Common.Models;
using Notification_Dal;

namespace Notification_Bl
{
    public class DependencyInjectionBl
    {
        public static void RegisterTypes(IServiceCollection container)
        {
            container.AddSingleton<INotificationsManager, NotificationsManager>();
            container.AddSingleton<ITokenManager, TokenManager>();
            container.AddSingleton<IDynamoDbRepository<Notification>, DynamoDbRepository<Notification>>();
        }
    }
}
