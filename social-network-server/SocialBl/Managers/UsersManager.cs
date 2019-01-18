using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using Social_Common.Interfaces.Managers;
using Social_Common.Interfaces.Repositories;
using Social_Common.Models;
using Social_Common.Models.Dtos;

namespace SocialBl.Managers
{
    public class UsersManager : IUsersManager
    {

        private string _notificationsUrl = ConfigurationManager.AppSettings["NotificationsServiceUrl"];

        IUsersRepository _usersRepository;
        public UsersManager(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
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

        public bool Follow(User user, string followedUserId)
        {
            try
            {
                _usersRepository.Follow(user.UserId, followedUserId);
            }
            catch (Exception e)
            {
                // todo logger
                return false;
            }
            using (var http = new HttpClient())
            {
                object followNoification = new { user, ReciverId = followedUserId };
                var response = http.PostAsJsonAsync(_notificationsUrl + "/Follow", followNoification).Result;
                if (!response.IsSuccessStatusCode)
                {
                    // todo logger
                }
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
                // todo logger
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
                // todo logger
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
                // todo logger
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
