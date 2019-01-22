using Social_Common.Interfaces.Helpers;
using System.Configuration;
using System.Net.Http;

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
