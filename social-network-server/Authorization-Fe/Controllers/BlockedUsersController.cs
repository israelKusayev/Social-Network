using Authorization_Common.Interfaces.Managers;
using Authorization_Fe.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Authorization_Fe.Controllers
{
    public class BlockedUsersController : ApiController
    {
        private IBlockedUsersManager _blockedUsersManager;

        public BlockedUsersController(IBlockedUsersManager blockedUsersManager)
        {
            _blockedUsersManager = blockedUsersManager;
        }

        [HttpPost]
        [JWTAdminAuth]
        [Route("/api/Blocked/Block/{userId}")]
        IHttpActionResult Block(string userId)
        {
            bool blocked = _blockedUsersManager.BlockUser(userId);
            if (blocked)
                return Ok("user has been blocked");
            else
                return BadRequest("user was not found or already blocked");
        }

        [HttpPost]
        [JWTAdminAuth]
        [Route("/api/Blocked/UnBlock/{userId}")]
        IHttpActionResult UnBlock(string userId)
        {
            bool blocked = _blockedUsersManager.UnBlockUser(userId);
            if (blocked)
                return Ok("user has been unblocked");
            else
                return BadRequest("user was not found or already unblocked");
        }
    }
}
