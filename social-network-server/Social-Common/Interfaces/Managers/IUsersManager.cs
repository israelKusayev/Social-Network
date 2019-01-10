using System.Collections.Generic;
using Social_Common.Models;

namespace Social_Common.Interfaces.Managers
{
    public interface IUsersManager
    {
        bool AddUser(User user);
        bool Follow(string userId, string followedUserId);
        List<User> GetFollowing(string userId);
        List<User> GetUsers(string searchQuery, string userId);
        bool IsFollow(string userId, string followedUserId);
        bool unfollow(string userId, string followedUserId);
    }
}