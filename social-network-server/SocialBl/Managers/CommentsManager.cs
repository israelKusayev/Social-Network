using Social_Common.Interfaces.Managers;
using Social_Common.Interfaces.Repositories;
using Social_Common.Models;
using Social_Common.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;

namespace SocialBl.Managers
{
    public class CommentsManager : ICommentsManager
    {
        private ICommentsRepository _commentsRepository;
        private IUsersRepository _usersRepository;
        private IAmazonS3Uploader _s3Uploader;

        private string _notificationsUrl = ConfigurationManager.AppSettings["NotificationsServiceUrl"];
        private string _serverToken = ConfigurationManager.AppSettings["ServerToken"];

        public CommentsManager(ICommentsRepository commentsRepository, IUsersRepository usersRepository,
            IAmazonS3Uploader s3Uploader)
        {
            _commentsRepository = commentsRepository;
            _usersRepository = usersRepository;
            _s3Uploader = s3Uploader;
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
                //TODO: add logger here
                return false;
            }
            using (var http = new HttpClient())
            {
                object commentNotification = new { commentId = guid, user, postId = commentDto.PostId, ReciverId = _usersRepository.GetPosting(commentDto.PostId).UserId };
                http.DefaultRequestHeaders.Add("x-auth-token", _serverToken);
                var response = http.PostAsJsonAsync(_notificationsUrl + "/CommentOnPost", commentNotification).Result;
                if (!response.IsSuccessStatusCode)
                {
                    // todo logger
                }

                foreach (var reference in commentDto.Referencing)
                {
                    _commentsRepository.CreateReference(guid, reference.UserId, reference.StartIndex, reference.EndIndex);

                    object referencigNoification = new { commentId = guid, user, postId = commentDto.PostId, ReciverId = reference.UserId };
                    var response2 = http.PostAsJsonAsync(_notificationsUrl + "/ReferenceInComment", referencigNoification).Result;
                    if (!response2.IsSuccessStatusCode)
                    {
                        // todo logger
                    }
                }
            }
            return true;
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
                //TODO: add logger here
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
