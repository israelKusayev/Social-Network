using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using log4net;
using Social_Common.Interfaces.Helpers;
using Social_Common.Interfaces.Managers;
using Social_Common.Interfaces.Repositories;
using Social_Common.Models;
using Social_Common.Models.Dtos;

namespace SocialBl.Managers
{
    public class UsersManager : IUsersManager
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IServerComunication _serverComunication;
        private readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public UsersManager(IUsersRepository usersRepository, IServerComunication serverComunication)
        {
            _usersRepository = usersRepository;
            _serverComunication = serverComunication;
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

                _log.Error(e);
                return false;
            }
        }

        public List<User> GetUsers(string searchQuery, string userId)
        {
            return _usersRepository.Find(searchQuery, userId);
        }

        public bool Follow(User user, string followedUserId)
        {
            try
            {
                _usersRepository.Follow(user.UserId, followedUserId);
            }
            catch (Exception e)
            {
                _log.Error(e);
                return false;
            }

            if (user.UserId != followedUserId)
            {
                object followNoification = new { user, ReciverId = followedUserId };
                _serverComunication.NotifyUser("/Follow", followNoification);
            }
            return true;
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
                _log.Error(e);
                return false;
            }
        }

        public bool BlockUser(string userId, string blockedUserId)
        {
            try
            {
                _usersRepository.Block(userId, blockedUserId);
                return true;
            }
            catch (Exception e)
            {
                _log.Error(e);
                return false;
            }
        }

        public List<User> GetBlockedUsers(string userId)
        {
            return _usersRepository.GetBlockedUsers(userId);
        }

        public bool UnblockUser(string userId, string blockedUserId)
        {
            try
            {
                _usersRepository.UnBlock(userId, blockedUserId);
                return true;
            }
            catch (Exception e)
            {
                _log.Error(e);
                return false;
            }
        }

        public bool IsBlocked(string userId, string otherUserId)
        {
            return _usersRepository.IsBlocked(userId, otherUserId);
        }

        public List<FollowersDTO> GetFollowers(string userId)
        {
            return _usersRepository.GetFollowers(userId);
        }
    }
}
