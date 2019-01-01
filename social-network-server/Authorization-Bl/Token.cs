﻿using Jose;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorization_Bl
{
    public class Token
    {
        public static string GenerateKey(string userId, string username)
        {

            long exp = (long)(DateTime.UtcNow.AddMinutes(15) - new DateTime(1970, 1, 1)).TotalSeconds;
            long iat = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;

            var payload = new Dictionary<string, object>()
                {
                    { "sub", userId },
                    { "exp",exp  },
                    { "iat",iat  },
                    { "aud","social network"  },
                    { "username",username}
                };


            byte[] secretKey = Encoding.ASCII.GetBytes("t2cVfLnZLjwhyS08X16C80WoDhfHDneILEmgKLvFut3RpBTijI4YYyUsV9mxMr-jpjHodhxbR5bTaSsb4Gxlhk0BwahTPuc9a6hRetdwZjp9opOHNyKJVG2fAws9MxFsoGUu9J7j-_C0VlzeA1bRLjZ34ZMjxslyMQ5VAEVwWs-pWorljHgfDAb-i0I7x7SiKBiKMJwvMYXN8Hb1sQcDcg3kWIiA5_QtLTE10bL3iZ9JyVz5G6AO0v2SVmAy-cmrdCWkqGhLCXH5TixpirgGjggAR4cVL8uFrQoaP3fxnzLJYk131XXlm3P8adwAuim7-UwIWjfazpQbwwY5I9kKDg");
            string token = Jose.JWT.Encode(payload, secretKey, JwsAlgorithm.HS256);
            return token;
        }
    }
}
