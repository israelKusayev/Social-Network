using Social_Common.Interfaces.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SocialBl.Helpers
{
    public class ServerComunication : IServerComunication
    {
        private readonly string _notificationsUrl = ConfigurationManager.AppSettings["NotificationsServiceUrl"];
        private readonly string _serverToken = ConfigurationManager.AppSettings["ServerToken"];

        public void NotifyUser(string action, object obj)
        {
            using (var http = new HttpClient())
            {
                http.DefaultRequestHeaders.Add("x-auth-token", _serverToken);
                var response = http.PostAsJsonAsync(_notificationsUrl + action, obj).Result;
                if (!response.IsSuccessStatusCode)
                {
                    // todo logger
                }
            }
        }
    }
}
