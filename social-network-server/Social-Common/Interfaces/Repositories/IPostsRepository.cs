using Social_Common.Models;
using Social_Common.Models.Dtos;

namespace Social_Common.Interfaces.Repositories
{
    public interface IPostsRepository
    {
        void Create(Post post, string postedByUserId);
        void CreateReference(string postId, string userId, int startIdx, int endIdx);
        PostListDto GetFeed(int startIdx, int count, string userId);
        ReturnedPostDto GetPost(string userId, string postId);
        string GetPostIdByCommentId(string commentId);
    }
}