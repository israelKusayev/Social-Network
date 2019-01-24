using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationFe.Hubs
{
    [Authorize]
    public class NotificationsHub : Hub
    {
        public NotificationsHub() : base()
        {

        }
    }
}
