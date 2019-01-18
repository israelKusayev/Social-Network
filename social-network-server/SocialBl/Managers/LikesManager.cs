using Newtonsoft.Json;
using Social_Common.Interfaces.Managers;
using Social_Common.Interfaces.Repositories;
using Social_Common.Models;
using Social_Common.Models.Dtos;
using System;
using System.Configuration;
using System.Net.Http;
using System.Text;

namespace SocialBl.Managers
{
    public class LikesManager : ILikesManager
    {
        string _notificationsUrl = ConfigurationManager.AppSettings["NotificationsServiceUrl"];
        private ILikesRepository _likesRepository;
        private IUsersRepository _usersRepository;
        private IPostsRepository _postsRepository;

        public LikesManager(ILikesRepository likesRepository, IUsersRepository usersRepository, IPostsRepository postsRepository)
        {
            _likesRepository = likesRepository;
            _usersRepository = usersRepository;
            _postsRepository = postsRepository;
        }

        public bool LikeComment(User user, string commentId)
        {
            try
            {
                _likesRepository.LikeComment(user.UserId, commentId);
            }
            catch (Exception e)
            {
                //TODO add log here
                return false;
            }

            using (var http = new HttpClient())
            {
                object obj = new { commentId, user, postId = _postsRepository.GetPostIdByCommentId(commentId), ReciverId = _usersRepository.GetCommentPublish(commentId).UserId };

                var response = http.PostAsJsonAsync(_notificationsUrl + "/UserLikeComment", obj).Result;
                if (!response.IsSuccessStatusCode)
                {
                    // todo logger
                }
            }
            return true;
        }

        public bool UnLikeComment(string userId, string commentId)
        {
            try
            {
                _likesRepository.UnLikeComment(userId, commentId);
                return true;
            }
            catch (Exception e)
            {
                //TODO add log here
                return false;
            }
        }

        public bool LikePost(User user, string postId)
        {
            try
            {
                _likesRepository.LikePost(user.UserId, postId);
            }
            catch (Exception e)
            {
                //TODO add log here
                return false;
            }
            using (var http = new HttpClient())
            {
                object obj = new { postId, user, ReciverId = _usersRepository.GetPosting(postId).UserId };
                var response = http.PostAsJsonAsync(_notificationsUrl + "/UserLikePost", obj).Result;
                if (!response.IsSuccessStatusCode)
                {
                    // todo logger
                }
            }
            return true;
        }

        public bool UnLikePost(string userId, string postId)
        {
            try
            {
                _likesRepository.UnLikePost(userId, postId);
                return true;
            }
            catch (Exception e)
            {
                //TODO add log here
                return false;
            }
        }
    }
}
