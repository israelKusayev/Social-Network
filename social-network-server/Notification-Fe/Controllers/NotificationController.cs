using System;
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
            // actionId = 0
            try
            {
                _hub.Clients.User(action.ReciverId).SendAsync("getNotification", action).Wait();
            }
            catch (Exception)
            {
                throw;
            }
            return Ok();
        }

        [HttpPost]
        [Route("UserLikeComment")]
        public ActionResult UserLikeComment(CommentActionDto action)
        {
            // actionId = 1
            return Ok();
        }

        [HttpPost]
        [Route("CommentOnPost")]
        public ActionResult CommentOnPost(CommentActionDto action)
        {
            // actionId = 2
            return Ok();
        }

        [HttpPost]
        [Route("ReferenceInPost")]
        public ActionResult ReferenceInPost(PostActionDto action)
        {
            // actionId = 3
            return Ok();
        }

        [HttpPost]
        [Route("ReferenceInComment")]
        public ActionResult ReferenceInComment(CommentActionDto action)
        {
            // actionId = 4
            return Ok();
        }

        [HttpPost]
        [Route("Follow")]
        public ActionResult Follow(UsersActionDto action)
        {
            // actionId = 5
            return Ok();
        }
    }
}