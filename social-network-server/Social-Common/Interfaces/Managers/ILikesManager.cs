using Social_Common.Models.Dtos;

namespace Social_Common.Interfaces.Managers
{
    public interface ILikesManager
    {
        bool LikeComment(LikeDto dto);
        bool LikePost(LikeDto dto);
        bool UnLikeComment(LikeDto dto);
        bool UnLikePost(LikeDto dto);
    }
}