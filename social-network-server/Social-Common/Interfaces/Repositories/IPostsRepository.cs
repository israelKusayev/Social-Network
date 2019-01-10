using Social_Common.Models;
using Social_Common.Models.Dtos;

namespace Social_Common.Interfaces.Repositories
{
    public interface IPostsRepository
    {
        void Create(Post post, string postedByUserId);
        ReturnedPostDto Get(string postId, string userId);
        PostListDto GetFeed(int startIdx, int count, string userId);
    }
}