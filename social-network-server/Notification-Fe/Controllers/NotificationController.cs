using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Notification_Common.Models.Dtos;
using NotificationFe.Hubs;

namespace Notification_Fe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        IHubContext<NotificationsHub> _hub;
        public NotificationController(IHubContext<NotificationsHub> hub)
        {
            _hub = hub;
        }


        [HttpPost]
        [Route("UserLikePost")]
        public ActionResult UserLikePost(PostActionDto action)
        {
            _hub.Clients.User(action.ReciverId).SendAsync("getNotification", action);
            return Ok();
        }
        [HttpPost]
        public ActionResult UserLikeComment()
        {
            return Ok();
        }

    }
}