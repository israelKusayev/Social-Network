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

        public List<User> GetUsers(string searchQuery, string userId)
        {
            return _usersRepository.Find(searchQuery, userId);
        }

        public bool Follow(string userId, string followedUserId)
        {
            try
            {
                _usersRepository.Follow(userId, followedUserId);
                return true;
            }
            catch (Exception e)
            {
                // todo logger
                return false;
            }
        }

        public List<User> GetFollowing(string userId)
        {
            return _usersRepository.GetFollowing(userId);
        }

        public bool IsFollow(string userId, string followedUserId)
        {
            return _usersRepository.IsFollow(userId, followedUserId);

        }

        public bool unfollow(string userId, string followedUserId)
        {
            try
            {
                _usersRepository.UnFollow(userId, followedUserId);
                return true;
            }
            catch (Exception e)
            {
                // todo logger
                return false;
            }
        }
    }
}
