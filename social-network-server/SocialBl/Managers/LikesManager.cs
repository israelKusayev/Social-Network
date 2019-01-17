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

        public LikesManager(ILikesRepository likesRepository, IUsersRepository usersRepository)
        {
            _likesRepository = likesRepository;
            _usersRepository = usersRepository;
        }

        public bool LikeComment(User user, string commentId)
        {
            try
            {
                _likesRepository.LikeComment(user.UserId, commentId);
                using (var http = new HttpClient())
                {
                    object obj = new { commentId, user, ReciverId = "123" };
                    var response = http.PostAsJsonAsync(_notificationsUrl + "/UserLikePost", obj).Result;
                }
                return true;
            }
            catch (Exception e)
            {
                //TODO add log here
                return false;
            }
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
                using (var http = new HttpClient())
                {
                    object obj = new { postId, user, ReciverId = _usersRepository.GetPosting(postId).UserId };
                    var response = http.PostAsJsonAsync(_notificationsUrl + "/UserLikePost", obj).Result;
                }
                return true;
            }
            catch (Exception e)
            {
                //TODO add log here
                return false;
            }
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
