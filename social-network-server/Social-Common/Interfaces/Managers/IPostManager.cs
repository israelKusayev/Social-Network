using Social_Common.Models;
using Social_Common.Models.Dtos;

namespace Social_Common.Interfaces.Managers
{
    public interface IPostManager
    {
        Post CreatePost(CreatePostDto postDto, string userId);
        PostListDto GetPosts(int start, int count, string userId);
        ReturnedPostDto GetPost(string userId, string postId);
    }
}