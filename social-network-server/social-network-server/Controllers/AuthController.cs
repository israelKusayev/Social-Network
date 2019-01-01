using social_network_server.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace social_network_server.Controllers
{
    public class AuthController : ApiController
    {
        [Route("api/register")]
        [HttpPost]
        public IHttpActionResult Register([FromBody] object s)
        {
            return Ok();
        }
    }
}
