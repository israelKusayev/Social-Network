﻿using Jose;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialBl.Managers
{
    public class TokenManager //: ITokenManager
    {
        public bool IsValid(string token)
        {

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