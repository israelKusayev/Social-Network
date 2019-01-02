using Microsoft.VisualStudio.TestTools.UnitTesting;
using Identity_Bl.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jose;
using System.Configuration;

namespace Identity_Bl.Managers.Tests
{
    [TestClass()]
    public class TokenManagerTests
    {
        [TestMethod()]
        public void IsValid_validTokenReturnsTrue()
        {
            //normal token
            string validToken = GenerateKey(DateTime.UtcNow,15, "social network");
            TokenManager tokenManager = new TokenManager();
            Assert.IsTrue(tokenManager.IsValid(validToken));
        }

        [TestMethod()]
        public void IsValid_invalidIatTokenReturnsFalse()
        {
            //token hasnt been isued yet
            string invalidToken = GenerateKey(DateTime.UtcNow.AddMinutes(10), 15, "social network");
            TokenManager tokenManager = new TokenManager();
            Assert.IsFalse(tokenManager.IsValid(invalidToken));
        }

        [TestMethod()]
        public void IsValid_invalidExpTokenReturnsFalse()
        {
            //token has expaired
            string invalidToken = GenerateKey(DateTime.UtcNow, -1, "social network");
            TokenManager tokenManager = new TokenManager();
            Assert.IsFalse(tokenManager.IsValid(invalidToken));
        }

        [TestMethod()]
        public void IsValid_invalidAudTokenReturnsFalse()
        {
            //token has wrong audit
            string invalidToken = GenerateKey(DateTime.UtcNow, 15, "social networks");
            TokenManager tokenManager = new TokenManager();
            Assert.IsFalse(tokenManager.IsValid(invalidToken));
        }

        [TestMethod()]
        public void IsValid_invalidSignatureTokenReturnsFalse()
        {
            //token has invalid signature
            string invalidToken = GenerateKey(DateTime.UtcNow, 15, "social network");
            string[] parts = invalidToken.Split('.');
            parts[2] = RandomString(parts[2].Length);
            invalidToken = string.Join(".", parts);
            TokenManager tokenManager = new TokenManager();
            Assert.IsFalse(tokenManager.IsValid(invalidToken));
        }

        private string GenerateKey(DateTime startTime, int ttl,string aud)
        {
            long exp = (long)(startTime.AddMinutes(ttl) - new DateTime(1970, 1, 1)).TotalSeconds;
            long iat = (long)(startTime - new DateTime(1970, 1, 1)).TotalSeconds;

            var payload = new Dictionary<string, object>()
                {
                    { "sub", "test" },
                    { "exp",exp  },
                    { "iat",iat  },
                    { "aud",aud  },
                    { "username","test"}
                };


            byte[] secretKey = Encoding.ASCII.GetBytes(ConfigurationManager.AppSettings["tokenSignKey"]);
            string token = Jose.JWT.Encode(payload, secretKey, JwsAlgorithm.HS256);

            return token;
        }

        private string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}