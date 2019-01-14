using Authorization_Common.Interfaces.Helppers;
using Jose;
using Newtonsoft.Json.Linq;
using System;
using System.Configuration;
using System.Text;

namespace Authorization_Bl.Helppers
{
    public class TokenValidator : ITokenValidator
    {
        public dynamic ValidaleRefreshToken(string token)
        {
            if (token == null)
                return null;
            dynamic data = ValidateSignature(token);
            int refrshTime = int.Parse(ConfigurationManager.AppSettings["RefreshTime"]) * 60;
            long now = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
            if (data.iat > now || data.exp < now - refrshTime ||
               data.aud != "social network")
            {
                return null;
            }
            return data;
        }

        public dynamic ValidaleToken(string token)
        {
            if (token == null)
                return null;
            dynamic data = ValidateSignature(token);

            long now = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
            if (data.iat > now || data.exp < now ||
                data.aud != "social network")
            {
                return null;
            }
            return data;
        }

        private dynamic ValidateSignature(string token)
        {
            if (token == null)
                return false;
            string key = ConfigurationManager.AppSettings["tokenSignKey"];
            string paylod = JWT.Decode(token, Encoding.ASCII.GetBytes(key));
            dynamic data = JObject.Parse(paylod);
            return data;
        }
    }
}
