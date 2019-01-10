using System.Collections.Generic;
using Social_Common.Models.Dtos;

namespace Social_Common.Interfaces.Managers
{
    public interface ICommentsManager
    {
        bool Create(CreateCommentDto commentDto, string userId);
        List<ReturnedCommentDto> GetByPost(string postId, string userId);
    }
}