using Social_Common.Models;
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

        public UsersController()
        {
            _usersManager = new UsersManager();
            _tokenManager = new TokenManager();
        }

        [HttpPost]
        public IHttpActionResult AddUser([FromBody] User user)
        {
            if (_usersManager.AddUser(user))
            {
                return Ok();
            }
            return InternalServerError();
        }

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
    }
}
