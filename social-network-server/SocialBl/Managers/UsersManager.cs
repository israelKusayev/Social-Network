using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Social_Common.Models;
using SocialDal.Repositories.Neo4j;

namespace SocialBl.Managers
{
    public class UsersManager
    {
        Neo4jUsersRepository _usersRepository;
        public UsersManager()
        {
            _usersRepository = new Neo4jUsersRepository();
        }
        public bool AddUser(User user)
        {
            try
            {
                _usersRepository.Create(user);
                return true;
            }
            catch (Exception e)
            {
                // todo logger
                return false;
            }
        }

        public object GetUsers(string searchQuery, string userId)
        {
            return _usersRepository.Find(searchQuery, userId);
        }
    }
}
