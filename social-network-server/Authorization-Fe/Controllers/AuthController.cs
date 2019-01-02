using Authorization_Bl;
using Authorization_Bl.Managers;
using Authorization_Common.Models;
using Authorization_Common.Models.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Authorization_Fe.Controllers
{
    public class AuthController : ApiController
    {
        AuthManager authManager;
        public AuthController()
        {
            authManager = new AuthManager();
        }
        [Route("api/register")]
        [HttpPost]
        public IHttpActionResult Register([FromBody] RegisterDTO model)
        {
            string error = Validations.ValidateRegister(model);
            if (error != null)
            {
                return BadRequest(error);
            }
            UserAuth auth;
            try
            {
                auth = authManager.Register(model);
                if (auth == null)
                {
                    return BadRequest("Username already exists in the db");
                }
            }
            catch (Exception)
            {
                return InternalServerError(new Exception("Something went worng"));
            }

            var token = Token.GenerateKey(auth.UserId, model.Username);

            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Headers.Add("x-auth-token", token);
            return ResponseMessage(response);
        }

        [Route("api/login")]
        [HttpPost]
        public IHttpActionResult Login([FromBody] LoginDTO model)
        {
            string error = Validations.ValidateLogin(model);
            if (error != null)
            {
                return BadRequest(error);
            }
            var auth = authManager.Login(model);
            if (auth == null)
            {
                return BadRequest("incorrect details");
            }

            var token = Token.GenerateKey(auth.UserId, model.Username);

            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Headers.Add("x-auth-token", token);
            return ResponseMessage(response);
        }

        [Route("api/LoginFacebook")]
        [HttpPost]
        public IHttpActionResult LoginFacebook([FromBody] FacebookLoginDTO model)
        {
            if (model == null)
            {
                return BadRequest("Token is missing");
            }

            UserFacebook facebookUser = authManager.LoginFacebook(model);

            var token = Token.GenerateKey(facebookUser.UserId, model.Username);

            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Headers.Add("x-auth-token", token);
            return ResponseMessage(response);
        }
    }
}
