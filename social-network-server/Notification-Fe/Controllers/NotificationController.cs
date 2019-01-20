using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Notification_Bl.Managers;
using Notification_Common.Models.Dtos;
using NotificationFe.Hubs;

namespace Notification_Fe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        IHubContext<NotificationsHub> _hub;
        NotificationsManager _notificationsManager;
        public NotificationController(IHubContext<NotificationsHub> hub, IConfiguration configuration)
        {
            _hub = hub;
            _notificationsManager = new NotificationsManager(configuration);
            //_notificationsManager.AddToHistory("")
            var res = configuration.GetValue<string>("key");
        }

        [HttpGet]
        [Route("GetNotifications/{userId}")]
        public IActionResult GetNotifications(string userId)
        {
            try
            {
                return Ok(_notificationsManager.GetNotifications(userId));
            }
            catch (Exception e)
            {
                //TODO: add logger
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("UserLikePost")]
        public IActionResult UserLikePost(PostActionDto action)
        {
            try
            {
                action.ActionId = 0;
                _notificationsManager.SendNotification(_hub, action.ReciverId, "getNotification", action);
                return Ok();
            }
            catch (Exception e)
            {
                //TODO: add logger
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("UserLikeComment")]
        public ActionResult UserLikeComment(CommentActionDto action)
        {
            try
            {
                action.ActionId = 1;
                _notificationsManager.SendNotification(_hub, action.ReciverId, "getNotification", action);
                return Ok();
            }
            catch (Exception e)
            {
                //TODO: add logger
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("CommentOnPost")]
        public ActionResult CommentOnPost(CommentActionDto action)
        {
            try
            {
                action.ActionId = 2;
                _notificationsManager.SendNotification(_hub, action.ReciverId, "getNotification", action);
                return Ok();
            }
            catch (Exception e)
            {
                //TODO: add logger
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("ReferenceInPost")]
        public ActionResult ReferenceInPost(PostActionDto action)
        {
            try
            {
                action.ActionId = 3;
                _notificationsManager.SendNotification(_hub, action.ReciverId, "getNotification", action);
                return Ok();
            }
            catch (Exception e)
            {
                //TODO: add logger
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("ReferenceInComment")]
        public ActionResult ReferenceInComment(CommentActionDto action)
        {
            try
            {
                action.ActionId = 4;
                _notificationsManager.SendNotification(_hub, action.ReciverId, "getNotification", action);
                return Ok();
            }
            catch (Exception e)
            {
                //TODO: add logger
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("Follow")]
        public ActionResult Follow(UsersActionDto action)
        {
            try
            {
                action.ActionId = 5;
                _notificationsManager.SendNotification(_hub, action.ReciverId, "getNotification", action);
                return Ok();
            }
            catch (Exception e)
            {
                //TODO: add logger
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}