using Identity_Bl.Managers;
using Identity_Common.Interfaces.Helppers;
using Identity_Common.Interfaces.Managers;
using Identity_Common.Loggers;
using Identity_Common.models;
using Identity_Fe.Attributes;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Linq;
using System.Web.Http;

namespace Identity_Fe.Controllers
{
    public class UsersIdentityController : ApiController
    {
        private IIdentiryManager _identityManager;
        private IRquestsValidator _rquestsValidator;
        private LoggerManager _logger;

        public UsersIdentityController(IIdentiryManager identiryManager, 
            IRquestsValidator rquestsValidator)
        {
            _identityManager = identiryManager;
            _rquestsValidator = rquestsValidator;
            string path = ConfigurationManager.AppSettings["IdentityLogsPath"];
            _logger = new LoggerManager(new FileLogger(), path);
        }

        [HttpPost]
        [JWTAuth]
        public IHttpActionResult Create([FromBody]User user)
        {
            try
            {
                string tokenId = new TokenManager().GetUserId(Request.Headers.GetValues("x-auth-token").First());
                if (user.UserId == null)
                    user.UserId = tokenId;
                string errors = _rquestsValidator.ValidateUser(user, tokenId);
                if(errors!=null)
                {
                    return BadRequest(errors);
                }
                var seccess = _identityManager.CreateUser(user);
                if (seccess)
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
                string err = e.ToString();
                _logger.Log(err);

                return InternalServerError();
            }
        }

        [HttpGet]
        [JWTAuth]
        public IHttpActionResult Get(string id)
        {
            try
            {
                //throw new Exception("test");
                var user = _identityManager.FindUser(id);
                if (user!=null)
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
                string err = e.ToString();
                _logger.Log(err);
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
                if (user.UserId == null)
                    user.UserId = tokenId;
                string errors = _rquestsValidator.ValidateUser(user, tokenId);
                if (errors != null)
                {
                    return BadRequest(errors);
                }
                var seccess = _identityManager.UpdateUser(user);
                if (seccess)
                {
                    return Ok("user data was updated seccusfuly");
                }
                else
                {
                    return BadRequest("could not update user date: user data does not exsists");
                }
            }
            catch (Exception e)
            {
                string err = e.ToString();
                _logger.Log(err);
                return InternalServerError();
            }
        }

    }
}
