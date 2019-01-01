using Identity_Common.interfaces;
using Identity_Common.models;

namespace Identity_Bl
{
    public class IdentiryManager : IIdentiryManager
    {
        private IUsersIdentityRepository _usersIdentityRepository;

        public IdentiryManager(IUsersIdentityRepository usersIdentityRepository)
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
    }
}
