using Social_Common.Models;
using Social_Fe.Attributes;
using SocialBl.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Social_Fe.Controllers
{
    public class UsersController : ApiController
    {
        UsersManager _usersManager;
        TokenManager _tokenManager;

        public UsersController(UsersManager usersManager, TokenManager tokenManager)
        {
            _usersManager = usersManager;
            _tokenManager = tokenManager;
        }

        [JWTAuth]
        [HttpPost]
        public IHttpActionResult AddUser([FromBody] User user)
        {
            if (_usersManager.AddUser(user))
            {
                return Ok();
            }
            return InternalServerError();
        }

        [JWTAuth]
        [HttpGet]
        [Route("api/users/getUsers/{searchQuery}")]
        public IHttpActionResult GetUsers(string searchQuery)
        {
            try
            {
                var token = Request.Headers.GetValues("x-auth-token").First();
                string userId = _tokenManager.GetUserId(token);
                return Ok(_usersManager.GetUsers(searchQuery, userId));
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }

        [JWTAuth]
        [HttpGet]
        [Route("api/users/isFollow/{followedUserId}")]
        public IHttpActionResult GetFollowing(string followedUserId)
        {
            var token = Request.Headers.GetValues("x-auth-token").First();
            string userId = _tokenManager.GetUserId(token);
            try
            {
                return Ok(_usersManager.IsFollow(userId, followedUserId));
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }

        [JWTAuth]
        [HttpGet]
        [Route("api/users/follow/{followedUserId}")]
        public IHttpActionResult Follow(string followedUserId)
        {
            var token = Request.Headers.GetValues("x-auth-token").First();
            string userId = _tokenManager.GetUserId(token);
            if (_usersManager.Follow(userId, followedUserId))
            {
                return Ok("You followed successfully");
            }
            return InternalServerError();
        }

        [JWTAuth]
        [HttpGet]
        [Route("api/users/unfollow/{followedUserId}")]
        public IHttpActionResult Unfollow(string followedUserId)
        {
            var token = Request.Headers.GetValues("x-auth-token").First();
            string userId = _tokenManager.GetUserId(token);
            if (_usersManager.unfollow(userId, followedUserId))
            {
                return Ok("You unfollowed successfully");
            }
            return InternalServerError();
        }

        [JWTAuth]
        [HttpGet]
        [Route("api/users/following")]
        public IHttpActionResult GetFollowing()
        {
            var token = Request.Headers.GetValues("x-auth-token").First();
            string userId = _tokenManager.GetUserId(token);
            try
            {
                return Ok(_usersManager.GetFollowing(userId));
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

    }
}
