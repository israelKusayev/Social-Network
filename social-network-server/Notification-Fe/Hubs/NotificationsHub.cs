using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationFe.Hubs
{
    public class NotificationsHub : Hub
    {
        public NotificationsHub() : base()
        {

        }

        
        //public void SendNotification(string data, string destination)
        //{
        //    Clients.User(destination).SendAsync("sendNotification", data);
        //}
    }
}
