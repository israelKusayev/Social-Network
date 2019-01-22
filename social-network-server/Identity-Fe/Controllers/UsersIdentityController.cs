using Identity_Bl.Managers;
using Identity_Common.Interfaces.Helppers;
using Identity_Common.Interfaces.Managers;
using Identity_Common.Loggers;
using Identity_Common.models;
using Identity_Fe.Attributes;
using log4net;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Web.Http;

namespace Identity_Fe.Controllers
{
    public class UsersIdentityController : ApiController
    {
        private readonly IIdentiryManager _identityManager;
        private readonly IRquestsValidator _rquestsValidator;

        private readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public UsersIdentityController(IIdentiryManager identiryManager, IRquestsValidator rquestsValidator)
        {
            _identityManager = identiryManager;
            _rquestsValidator = rquestsValidator;
        }


        [HttpPost]
        [JWTAuth]
        public IHttpActionResult Create([FromBody]User user)
        {
            try
            {
                string tokenId = new TokenManager().GetUserId(Request.Headers.GetValues("x-auth-token").First());
                if (user.UserId == null) user.UserId = tokenId;
                string errors = _rquestsValidator.ValidateUser(user, tokenId);
                if (errors != null) return BadRequest(errors);

                var success = _identityManager.CreateUser(user);
                if (success)
                {
                    return Ok("user data was created seccusfuly");
                }
                else
                {
                    return BadRequest("could not create user date: user data already exsists");
                }
            }
            catch (Exception e)
            {
                _log.Error(e.ToString());
                return InternalServerError();
            }
        }

        [HttpGet]
        [JWTAuth]
        public IHttpActionResult Get(string id)
        {
            try
            {
                string token = Request.Headers.GetValues("x-auth-token").First();
                string userId = new TokenManager().GetUserId(token);
                if (userId != id)
                {
                    if (_identityManager.IsBlocked(token, userId, id))
                    {
                        return BadRequest("User is blocked");
                    }
                }

                var user = _identityManager.FindUser(id);
                if (user != null)
                {
                    return Ok(JsonConvert.SerializeObject(user));
                }
                else
                {
                    return BadRequest("could not find user date with gived id");
                }
            }
            catch (Exception e)
            {
                _log.Error(e.ToString());
                return InternalServerError();
            }
        }

        [HttpPut]
        [JWTAuth]
        public IHttpActionResult Update([FromBody]User user)
        {
            try
            {
                string tokenId = new TokenManager().GetUserId(Request.Headers.GetValues("x-auth-token").First());
                if (user.UserId == null) user.UserId = tokenId;
                string errors = _rquestsValidator.ValidateUser(user, tokenId);
                if (errors != null) return BadRequest(errors);

                var seccess = _identityManager.UpdateUser(user);
                if (seccess)
                {
                    return Ok("user data was updated succesfuly");
                }
                else
                {
                    return BadRequest("could not update user date: user data does not exists");
                }
            }
            catch (Exception e)
            {
                _log.Error(e.ToString());
                return InternalServerError();
            }
        }
    }
}
