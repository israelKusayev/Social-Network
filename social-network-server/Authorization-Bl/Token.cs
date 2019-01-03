﻿using Authorization_Common.Interfaces;
using Authorization_Common.Interfaces.Repositories;
using Authorization_Common.Models;
using Jose;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Authorization_Bl
{
    public class Token : IToken
    {
        private IDynamoDbRepository<TokenHistory> _tokenReposirory;
        public Token(IDynamoDbRepository<TokenHistory> tokenReposirory)
        {
            _tokenReposirory = tokenReposirory;
        }
        public string GenerateKey(string userId, string username)
        {
            int ttl = int.Parse(ConfigurationManager.AppSettings["TokenTTL"]);
            long exp = (long)(DateTime.UtcNow.AddMinutes(15) - new DateTime(1970, 1, 1)).TotalSeconds;
            long iat = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;

            var payload = new Dictionary<string, object>()
                {
                    { "sub", userId },
                    { "exp",exp  },
                    { "iat",iat  },
                    { "aud","social network"},
                    { "username",username}
                };

            string key = ConfigurationManager.AppSettings["tokenSignKey"];
            byte[] secretKey = Encoding.ASCII.GetBytes(key);
            string token = Jose.JWT.Encode(payload, secretKey, JwsAlgorithm.HS256);

            TokenHistory history = new TokenHistory() { Token = token, UserId = userId, TimeStamp = iat };
            _tokenReposirory.Add(history);

            return token;
        }
    }
}
