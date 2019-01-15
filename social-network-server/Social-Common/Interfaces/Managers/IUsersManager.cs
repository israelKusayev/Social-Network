using System.Collections.Generic;
using Social_Common.Models;
using Social_Common.Models.Dtos;

namespace Social_Common.Interfaces.Managers
{
    public interface IUsersManager
    {
        bool AddUser(User user);
        bool BlockUser(string userId, string blockedUserId);
        bool Follow(string userId, string followedUserId);
        List<User> GetBlockedUsers(string userId);
        List<FollowersDTO> GetFollowers(string userId);
        List<User> GetFollowing(string userId);
        List<User> GetUsers(string searchQuery, string userId);
        bool IsFollow(string userId, string followedUserId);
        bool UnblockUser(string userId, string blockedUserId);
        bool unfollow(string userId, string followedUserId);
        bool IsBlocked(string userId, string otherUserId);
    }
}