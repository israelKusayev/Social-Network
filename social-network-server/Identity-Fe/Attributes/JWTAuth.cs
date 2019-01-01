using Identity_Bl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Identity_Fe.Attributes
{
    public class JWTAuth : AuthorizeAttribute
    {
        private TokenManager _tokenManager;
        public JWTAuth(TokenManager tokenManager)
        {
            _tokenManager = tokenManager;
        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            bool isAuthorized = base.IsAuthorized(actionContext);
            string token = actionContext.Request.Headers.GetValues("x-auth-token").First();
            bool validToken = _tokenManager.IsValid(token);
            return isAuthorized && validToken;
        }
    }
}