using log4net;
using Social_Common.Interfaces.Helpers;
using Social_Common.Interfaces.Managers;
using Social_Common.Interfaces.Repositories;
using Social_Common.Models;
using Social_Common.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Reflection;

namespace SocialBl.Managers
{
    public class CommentsManager : ICommentsManager
    {
        private readonly ICommentsRepository _commentsRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly IAmazonS3Uploader _s3Uploader;
        private readonly IServerComunication _serverComunication;
        private readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public CommentsManager(ICommentsRepository commentsRepository, IUsersRepository usersRepository,
            IAmazonS3Uploader s3Uploader, IServerComunication serverComunication)
        {
            _commentsRepository = commentsRepository;
            _usersRepository = usersRepository;
            _s3Uploader = s3Uploader;
            _serverComunication = serverComunication;
        }

        public bool Create(CreateCommentDto commentDto, User user)
        {
            string guid = Guid.NewGuid().ToString();
            try
            {
                string url = _s3Uploader.UploadFile(commentDto.Image, guid);
                Comment comment = CreateComment(commentDto, guid, url);
                _commentsRepository.Create(comment, user.UserId, commentDto.PostId);
            }
            catch (Exception e)
            {
                _log.Error(e);
                return false;
            }

            NotifyPostingUser(commentDto, user, guid);

            CreateUserReferences(commentDto, user, guid);
            return true;
        }

        private void NotifyPostingUser(CreateCommentDto commentDto, User user, string guid)
        {
            var PostingId = _usersRepository.GetPosting(commentDto.PostId).UserId;
            if (user.UserId != PostingId)
            {
                object commentNotification = new { commentId = guid, user, postId = commentDto.PostId, ReciverId = PostingId };
                _serverComunication.NotifyUser("/CommentOnPost", commentNotification);
            }
        }

        private void CreateUserReferences(CreateCommentDto commentDto, User user, string guid)
        {
            foreach (var reference in commentDto.Referencing)
            {
                _commentsRepository.CreateReference(guid, reference.UserId, reference.StartIndex, reference.EndIndex);
                if (user.UserId != reference.UserId)
                {
                    object referencigNoification = new { commentId = guid, user, postId = commentDto.PostId, ReciverId = reference.UserId };
                    _serverComunication.NotifyUser("/ReferenceInComment", referencigNoification);
                }
            }
        }

        public List<ReturnedCommentDto> GetByPost(string postId, string userId)
        {
            try
            {
                return _commentsRepository.GetComments(userId, postId);
            }
            catch (Exception e)
            {
                _log.Error(e);
                return null;
            }
        }

        private Comment CreateComment(CreateCommentDto commentDto, string guid, string url)
        {
            return new Comment()
            {
                CommentId = guid,
                Content = commentDto.Content,
                CreatedOn = DateTime.UtcNow,
                ImgUrl = url
            };
        }
    }
}
