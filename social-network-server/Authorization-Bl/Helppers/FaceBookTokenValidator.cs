using Authorization_Common.Interfaces.Helppers;
using Authorization_Common.Models.DTO;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Dynamic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;

namespace Authorization_Bl.Helppers
{
    public class FaceBookTokenValidator : IFaceBookTokenValidator
    {
        public FacebookLoginDTO ValidateAndGet(string facebookToken)
        {
            string fbAppSecret = ConfigurationManager.AppSettings["FbAppSecret"];
            string prof = ComputeHmacSha256Hash(facebookToken, fbAppSecret);
            string url = "https://graph.facebook.com/v2.5/me?fields=id,email,name&access_token=" + facebookToken
                + "&appsecret_proof=" + prof;
            using (var client = new HttpClient())
            {
                var res = client.GetAsync(url).Result;
                if (res.IsSuccessStatusCode)
                {
                    var json = res.Content.ReadAsStringAsync().Result;
                    dynamic payload = JObject.Parse(json);
                    FacebookLoginDTO data = FillFaceBookDto(payload);
                    return data;
                }
            }
            return null;
        }

        private FacebookLoginDTO FillFaceBookDto(dynamic payload)
        {
            FacebookLoginDTO data = new FacebookLoginDTO();
            data.FacebookId = payload.id;
            if (IsPropertyExist(payload, "email"))
            {
                data.Email = payload.email;
            }
            if (IsPropertyExist(payload, "name"))
            {
                data.Username = payload.name;
            }

            return data;
        }

        private string ComputeHmacSha256Hash(string valueToHash, string key)
        {
            byte[] keyBytes = Encoding.ASCII.GetBytes(key);
            byte[] valueBytes = Encoding.ASCII.GetBytes(valueToHash);
            byte[] tokenBytes = new HMACSHA256(keyBytes).ComputeHash(valueBytes);
            valueBytes = null;
            keyBytes = null;

            StringBuilder token = new StringBuilder();
            foreach (byte b in tokenBytes)
            {
                token.AppendFormat("{0:x2}", b);
            }
            tokenBytes = null;

            return token.ToString();
        }

        private bool IsPropertyExist(dynamic settings, string name)
        {
            try
            {
                var x = settings[name];
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
