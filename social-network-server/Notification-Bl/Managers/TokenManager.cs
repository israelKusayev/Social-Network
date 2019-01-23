using Newtonsoft.Json.Linq;
using Jose;
using System;
using System.Text;
using Notification_Common.Interfaces.Managers;

namespace Notification_Bl.Managers
{
    public class TokenManager : ITokenManager
    {
        public bool IsValid(string token)
        {
            if (token == null||token=="null")
                return false;
            string secretKey = "t2cVfLnZLjwhyS08X16C80WoDhfHDneILEmgKLvFut3RpBTijI4YYyUsV9mxMr-jpjHodhxbR5bTaSsb4Gxlhk0BwahTPuc9a6hRetdwZjp9opOHNyKJVG2fAws9MxFsoGUu9J7j-_C0VlzeA1bRLjZ34ZMjxslyMQ5VAEVwWs-pWorljHgfDAb-i0I7x7SiKBiKMJwvMYXN8Hb1sQcDcg3kWIiA5_QtLTE10bL3iZ9JyVz5G6AO0v2SVmAy-cmrdCWkqGhLCXH5TixpirgGjggAR4cVL8uFrQoaP3fxnzLJYk131XXlm3P8adwAuim7-UwIWjfazpQbwwY5I9kKDg";
            try
            {
                string paylod = JWT.Decode(token, Encoding.ASCII.GetBytes(secretKey));
                dynamic data = JObject.Parse(paylod);

                long now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                //long now = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
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
            string secretKey = "t2cVfLnZLjwhyS08X16C80WoDhfHDneILEmgKLvFut3RpBTijI4YYyUsV9mxMr-jpjHodhxbR5bTaSsb4Gxlhk0BwahTPuc9a6hRetdwZjp9opOHNyKJVG2fAws9MxFsoGUu9J7j-_C0VlzeA1bRLjZ34ZMjxslyMQ5VAEVwWs-pWorljHgfDAb-i0I7x7SiKBiKMJwvMYXN8Hb1sQcDcg3kWIiA5_QtLTE10bL3iZ9JyVz5G6AO0v2SVmAy-cmrdCWkqGhLCXH5TixpirgGjggAR4cVL8uFrQoaP3fxnzLJYk131XXlm3P8adwAuim7-UwIWjfazpQbwwY5I9kKDg";
            string paylod = JWT.Decode(token, Encoding.ASCII.GetBytes(secretKey));
            dynamic obj = JObject.Parse(paylod);
            return obj.sub;
        }
    }
}
