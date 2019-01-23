using Jose;
using Newtonsoft.Json.Linq;
using Social_Common.Interfaces.Managers;
using Social_Common.Models;
using System;
using System.Configuration;
using System.Text;

namespace SocialBl.Managers
{
    public class TokenManager : ITokenManager
    {
        public bool IsValid(string token)
        {
            if (token == null || token == "null")
                return false;
            string secretKey = ConfigurationManager.AppSettings["tokenSignKey"];
            try
            {
                string paylod = JWT.Decode(token, Encoding.ASCII.GetBytes(secretKey));
                dynamic data = JObject.Parse(paylod);
                long now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                if (data.iat > now || data.exp < now ||
                    data.aud != "social network")
                {
                    //invalid token
                    return false;
                }
                return true;
            }
            catch (InvalidAlgorithmException) { return false; }
            catch (IntegrityException) { return false; }
            catch (JoseException) { return false; }
            catch (ArgumentOutOfRangeException) { return false; }

        }

        public string GetUserId(string token)
        {
            string secretKey = ConfigurationManager.AppSettings["tokenSignKey"];
            string paylod = JWT.Decode(token, Encoding.ASCII.GetBytes(secretKey));
            dynamic obj = JObject.Parse(paylod);
            return obj.sub;
        }

        public User GetUser(string token)
        {
            string secretKey = ConfigurationManager.AppSettings["tokenSignKey"];
            string paylod = JWT.Decode(token, Encoding.ASCII.GetBytes(secretKey));
            dynamic obj = JObject.Parse(paylod);
            return new User { UserId = obj.sub, UserName = obj.username };
        }
    }
}
