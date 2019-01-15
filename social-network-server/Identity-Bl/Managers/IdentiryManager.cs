using Identity_Common.Interfaces.Managers;
using Identity_Common.Interfaces.Repositories;
using Identity_Common.models;
using System.Configuration;
using System.Net.Http;

namespace Identity_Bl.Managers
{
    public class IdentiryManager : IIdentiryManager
    {
        private IDynamoDbRepository<User> _usersIdentityRepository;

        public IdentiryManager(IDynamoDbRepository<User> usersIdentityRepository)
        {
            _usersIdentityRepository = usersIdentityRepository;
        }

        public User FindUser(string userId)
        {
            return _usersIdentityRepository.Get(userId);
        }

        public bool UpdateUser(User user)
        {
            return _usersIdentityRepository.Update(user);
        }

        public bool CreateUser(User user)
        {
            return _usersIdentityRepository.Add(user);
        }

        public bool IsBlocked(string token, string userId, string otherId)
        {
            string url = ConfigurationManager.AppSettings["SocialServiceUrl"];
            using (var http = new HttpClient())
            {
                http.DefaultRequestHeaders.Add("x-auth-token", token);
                var response = http.GetAsync(url + "/users/isBlocked/" + otherId).Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<bool>().Result;
                }
                return false;
            }
        }
    }
}
