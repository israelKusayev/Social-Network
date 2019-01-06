using Authorization_Bl.Helppers;
using Microsoft.CSharp.RuntimeBinder;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Authorization_Fe.Attributes
{
    public class JWTAdminAuth : AuthorizeAttribute
    {
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Contains("x-auth-token"))
            {
                string token = actionContext.Request.Headers.GetValues("x-auth-token").First();
                dynamic data = new TokenValidator().ValidaleToken(token);
                try
                {
                    return data != null ? (bool)data.IsAdmin : false;
                }
                catch (RuntimeBinderException)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}