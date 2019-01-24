using Newtonsoft.Json.Linq;
using Jose;
using System;
using System.Text;
using Notification_Common.Interfaces.Managers;
using Microsoft.Extensions.Configuration;

namespace Notification_Bl.Managers
{
    public class TokenManager : ITokenManager
    {
        private readonly IConfiguration _configuration;
        public TokenManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetUserId(string token)
        {
            string secretKey = _configuration["Key"];
            string paylod = JWT.Decode(token, Encoding.ASCII.GetBytes(secretKey));
            dynamic obj = JObject.Parse(paylod);
            return obj.sub;
        }
    }
}
