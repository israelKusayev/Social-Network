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
        Token _token;
        AuthManager _authManager;
        public AuthController()
        {
            _authManager = new AuthManager();
            _token = new Token();
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
            string token;
            try
            {
                auth = _authManager.Register(model);
                if (auth == null)
                {
                    return BadRequest("Username already exists in the db");
                }
                token = _token.GenerateKey(auth.UserId, model.Username);
                _authManager.AddUserToDb(auth.UserId, model.Email, token);
            }
            catch (Exception)
            {
                return InternalServerError(new Exception("Something went worng"));
            }

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
            var auth = _authManager.Login(model);
            if (auth == null)
            {
                return BadRequest("incorrect details");
            }

            var token = _token.GenerateKey(auth.UserId, model.Username);

            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Headers.Add("x-auth-token", token);
            return ResponseMessage(response);
        }

        [Route("api/loginFacebook")]
        [HttpPost]
        public IHttpActionResult LoginFacebook([FromBody] FacebookLoginDTO model)
        {
            if (model == null)
            {
                return BadRequest("Token is missing");
            }

            UserFacebook facebookUser = _authManager.LoginFacebook(model);

            var token = _token.GenerateKey(facebookUser.UserId, model.Username);
            _authManager.AddUserToDb(facebookUser.UserId, model.Email, token);

            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Headers.Add("x-auth-token", token);
            return ResponseMessage(response);
        }

        [HttpPut]
        [Route("api/resetPassword")]
        public IHttpActionResult ResetPassword(ResetPasswordDTO model)
        {
            string error = Validations.ValidateResetPassword(model);
            if (error != null)
            {
                return BadRequest(error);
            }

            try
            {
                if (!_authManager.ResetPassword(model))
                {
                    return BadRequest("incorrect details");
                }
                return Ok();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
