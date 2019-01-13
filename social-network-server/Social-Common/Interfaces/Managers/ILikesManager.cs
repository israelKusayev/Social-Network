using Social_Common.Models.Dtos;

namespace Social_Common.Interfaces.Managers
{
    public interface ILikesManager
    {
        bool LikeComment(string userId, string commentId);
        bool LikePost(string userId, string postId);
        bool UnLikeComment(string userId, string commentId);
        bool UnLikePost(string userId, string postId);
    }
}