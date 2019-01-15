using System.Collections.Generic;
using Social_Common.Models;
using Social_Common.Models.Dtos;

namespace Social_Common.Interfaces.Repositories
{
    public interface IUsersRepository
    {
        void Block(string blockingUserId, string blockedUserId);
        void Create(User user);
        List<User> Find(string name, string userId);
        void Follow(string followingUserId, string followedUserId);
        User Get(string userId, string myId);
        List<User> GetFollowing(string userId);
        bool IsFollow(string userId, string followedUserId);
        void UnBlock(string blockingUserId, string blockedUserId);
        void UnFollow(string followingUserId, string followedUserId);
        List<User> GetBlockedUsers(string userId);
        List<FollowersDTO> GetFollowers(string userId);
        bool IsBlocked(string userId, string otherUserId);
    }
}