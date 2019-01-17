using Jose;
using Newtonsoft.Json.Linq;
using Notifications_Common.Interfaces.Managers;
using System;
using System.Configuration;
using System.Text;

namespace Notifications_Bl.Managers
{
    public class TokenManager : ITokenManager
    {
        public bool IsValid(string token)
        {
            if (token == null||token=="null")
                return false;
            string secretKey = ConfigurationManager.AppSettings["tokenSignKey"];
            try
            {
                string paylod = JWT.Decode(token, Encoding.ASCII.GetBytes(secretKey));
                dynamic data = JObject.Parse(paylod);

                long now = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
                if (data.iat > now || data.exp < now ||
                    data.aud != "social network")
                {
                    //invalid token
                    return false;
                }
                return true;
            }
            catch (InvalidAlgorithmException)
            {
                return false;
            }
            catch (IntegrityException)
            {
                return false;
            }
            catch (JoseException)
            {
                return false;
            }
        }

        public string GetUserId(string token)
        {
            string secretKey = ConfigurationManager.AppSettings["tokenSignKey"];
            string paylod = JWT.Decode(token, Encoding.ASCII.GetBytes(secretKey));
            dynamic obj = JObject.Parse(paylod);
            return obj.sub;
        }
    }
}
