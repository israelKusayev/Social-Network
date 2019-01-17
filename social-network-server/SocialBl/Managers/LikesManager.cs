using Newtonsoft.Json;
using Social_Common.Interfaces.Managers;
using Social_Common.Interfaces.Repositories;
using Social_Common.Models;
using Social_Common.Models.Dtos;
using System;
using System.Net.Http;
using System.Text;

namespace SocialBl.Managers
{
    public class LikesManager : ILikesManager
    {
        private ILikesRepository _likesRepository;

        public LikesManager(ILikesRepository likesRepository)
        {
            _likesRepository = likesRepository;
        }

        public bool LikeComment(User user, string commentId)
        {
            try
            {
                _likesRepository.LikeComment(user.UserId, commentId);
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
                    object a = new { postId, user, reciver = "123" };
                    var response = http.PostAsync("https://localhost:44340/api/Notification/UserLikePost", new StringContent(JsonConvert.SerializeObject(a), Encoding.UTF8, "application/json")).Result;
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
