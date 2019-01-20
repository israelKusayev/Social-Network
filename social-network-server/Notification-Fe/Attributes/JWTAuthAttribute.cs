using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notification_Fe.Attributes
{
    //public class JWTAuthAttribute : TypeFilterAttribute
    //{
    //    public JWTAuthAttribute() : base(typeof(JwtAuthorizeActionFilter))
    //    {
               
    //    }
    //}

    //public class JwtAuthorizeActionFilter : IAsyncActionFilter
    //{
        //private readonly IValidateBearerToken _authToken;
       // public AuthorizeActionFilter(IValidateBearerToken authToken)
        //{
       //     _authToken = authToken;
        //}

        //public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        //{
        //    const string AUTHKEY = "authorization";
        //    var headers = context.HttpContext.Request.Headers;
        //    if (headers.ContainsKey(AUTHKEY))
        //    {
        //        bool isAuthorized = _authToken.Validate(headers[AUTHKEY]);
        //        if (!isAuthorized)
        //            context.Result = new UnauthorizedResult();
        //        else
        //            await next();
        //    }
        //    else
        //        context.Result = new UnauthorizedResult();
        //}
    //}
}
