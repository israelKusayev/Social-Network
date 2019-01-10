namespace Social_Common.Interfaces.Repositories
{
    public interface ILikesRepository
    {
        void LikeComment(string userId, string CommentId);
        void LikePost(string userId, string PostId);
        void UnLikeComment(string userId, string CommentId);
        void UnLikePost(string userId, string PostId);
    }
}