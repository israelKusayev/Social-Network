using Social_Common.Models;
using Social_Common.Models.Dtos;

namespace Social_Common.Interfaces.Managers
{
    public interface ILikesManager
    {
        bool LikeComment(User user, string commentId);
        bool LikePost(User user, string postId);
        bool UnLikeComment(string userId, string commentId);
        bool UnLikePost(string userId, string postId);
    }
}