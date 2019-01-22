using System;
using System.Reflection;
using log4net;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Notification_Bl.Managers;
using Notification_Common.Interfaces.Managers;
using Notification_Common.Models.Dtos;
using NotificationFe.Hubs;

namespace Notification_Fe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IHubContext<NotificationsHub> _hub;
        private readonly ITokenManager _tokenManager;
        private readonly INotificationsManager _notificationsManager;
        private readonly ILogger<NotificationController> _logger;

        public NotificationController(IHubContext<NotificationsHub> hub, IConfiguration configuration, INotificationsManager notificationsManager, ITokenManager tokenManager, ILogger<NotificationController> logger)
        {
            _logger = logger;
            _hub = hub;
            _notificationsManager = notificationsManager;
            _tokenManager = tokenManager;
        }

        [Authorize]
        [HttpGet]
        [Route("GetNotifications")]
        public IActionResult GetNotifications()
        {
            try
            {
                var token = Request.Headers["x-auth-token"][0];

                var userId = _tokenManager.GetUserId(token);
                return Ok(_notificationsManager.GetNotifications(userId));
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Authorize(Policy = "ServerOnly")]
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
                _logger.LogError(e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Authorize(Policy = "ServerOnly")]
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
                _logger.LogError(e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Authorize(Policy = "ServerOnly")]
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
                _logger.LogError(e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Authorize(Policy = "ServerOnly")]
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
                _logger.LogError(e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Authorize(Policy = "ServerOnly")]
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
                _logger.LogError(e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Authorize(Policy = "ServerOnly")]
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
                _logger.LogError(e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Authorize(Policy = "ServerOnly")]
        [Route("RecommendFollwers")]
        public ActionResult RecommendFollwers([FromBody]List<FollowRecommendationDto> followRecommendations)
        {
            try
            {
                foreach (FollowRecommendationDto rec in followRecommendations)
                {
                    object action = new
                    {
                        actionId = 6,
                        recomendedId = rec.RecommededUserId
                    };
                    _notificationsManager.SendNotification(_hub, rec.UserId, "getNotification", action);
                }
                    return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}