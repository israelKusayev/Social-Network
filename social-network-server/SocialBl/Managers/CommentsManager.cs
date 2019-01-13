using Social_Common.Interfaces.Managers;
using Social_Common.Interfaces.Repositories;
using Social_Common.Models;
using Social_Common.Models.Dtos;
using System;
using System.Collections.Generic;

namespace SocialBl.Managers
{
    public class CommentsManager : ICommentsManager
    {
        private ICommentsRepository _commentsRepository;
        private IAmazonS3Uploader _s3Uploader;

        public CommentsManager(ICommentsRepository commentsRepository,
            IAmazonS3Uploader s3Uploader)
        {
            _commentsRepository = commentsRepository;
            _s3Uploader = s3Uploader;
        }

        public bool Create(CreateCommentDto commentDto, string userId)
        {
            try
            {
                string guid = Guid.NewGuid().ToString();
                string url = _s3Uploader.UploadFile(commentDto.Image, guid);
                Comment comment = new Comment()
                {
                    CommentId = guid,
                    Content = commentDto.Content,
                    CreatedOn = DateTime.UtcNow,
                    ImgUrl = url
                };
                _commentsRepository.Create(comment, userId, commentDto.PostId);
                return true;
            }
            catch (Exception e)
            {
                //TODO: add logger here
                return false;
            }
        }

        public List<ReturnedCommentDto> GetByPost(string postId, string userId)
        {
            try
            {
                return _commentsRepository.GetComments(userId, postId);
            }
            catch
            (Exception e)
            {
                //TODO: add logger her
                return null;
            }
        }
    }
}
