using Identity_Bl.Managers;
using Identity_Common.interfaces;
using Identity_Common.models;
using Identity_Fe.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Identity_Fe.Controllers
{
    public class UsersIdentityController : ApiController
    {
        private IIdentiryManager _identityManager;
        private IRquestsValidator _rquestsValidator;
        public UsersIdentityController(IIdentiryManager identiryManager, 
            IRquestsValidator rquestsValidator)
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
                //TODO: add logger here
                return InternalServerError();
            }
        }

        [HttpGet]
        [JWTAuth]
        public IHttpActionResult Get(string id)
        {
            try
            {
                var user = _identityManager.FindUser(id);
                if (user!=null)
                {
                    return Ok(user);
                }
                else
                {
                    return BadRequest("could not find user date with gived id");
                }
            }
            catch (Exception e)
            {
                //TODO: add logger here
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
                //TODO: add logger here
                return InternalServerError();
            }
        }

    }
}
