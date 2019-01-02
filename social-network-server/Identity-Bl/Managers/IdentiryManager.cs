using Identity_Common.interfaces;
using Identity_Common.models;

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
    }
}
