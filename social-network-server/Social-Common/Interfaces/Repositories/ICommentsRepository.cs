using System.Collections.Generic;
using Social_Common.Models;
using Social_Common.Models.Dtos;

namespace Social_Common.Interfaces.Repositories
{
    public interface ICommentsRepository
    {
        void Create(Comment comment, string userId, string postId);
        void CreateReference(string commentId, string userId, int startIdx, int endIdx);
        List<ReturnedCommentDto> GetComments(string userId, string postId);
    }
}