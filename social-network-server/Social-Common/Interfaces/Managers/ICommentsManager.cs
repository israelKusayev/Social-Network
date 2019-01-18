using System.Collections.Generic;
using Social_Common.Models;
using Social_Common.Models.Dtos;

namespace Social_Common.Interfaces.Managers
{
    public interface ICommentsManager
    {
        bool Create(CreateCommentDto commentDto, User user);
        List<ReturnedCommentDto> GetByPost(string postId, string userId);
    }
}