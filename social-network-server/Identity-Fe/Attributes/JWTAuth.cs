using Identity_Bl.Managers;
using Identity_Common.interfaces;
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
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Contains("x-auth-token"))
            {
                string token = actionContext.Request.Headers.GetValues("x-auth-token").First();
                return new TokenManager().IsValid(token);
            }
            else
            {
                return false;
            }
        }
    }
}