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
        public UsersController()
        {
            _usersManager = new UsersManager();
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
                return Ok(_usersManager.GetUsers(searchQuery));
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }
    }
}
