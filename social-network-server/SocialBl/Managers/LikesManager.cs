using log4net;
using Newtonsoft.Json;
using Social_Common.Interfaces.Helpers;
using Social_Common.Interfaces.Managers;
using Social_Common.Interfaces.Repositories;
using Social_Common.Models;
using Social_Common.Models.Dtos;
using System;
using System.Configuration;
using System.Net.Http;
using System.Reflection;
using System.Text;

namespace SocialBl.Managers
{
    public class LikesManager : ILikesManager
    {
        private readonly ILikesRepository _likesRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly IPostsRepository _postsRepository;
        private readonly IServerComunication _serverComunication;
        private readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public LikesManager(ILikesRepository likesRepository, IUsersRepository usersRepository, IPostsRepository postsRepository, IServerComunication serverComunication)
        {
            _likesRepository = likesRepository;
            _usersRepository = usersRepository;
            _postsRepository = postsRepository;
            _serverComunication = serverComunication;
        }

        public bool LikeComment(User user, string commentId)
        {
            try
            {
                _likesRepository.LikeComment(user.UserId, commentId);
            }
            catch (Exception e)
            {
                _log.Error(e);
                return false;
            }

            string reciverId = _usersRepository.GetCommentPublish(commentId).UserId;
            if (user.UserId != reciverId)
            {
                object obj = new { commentId, user, postId = _postsRepository.GetPostIdByCommentId(commentId), ReciverId = reciverId };
                _serverComunication.NotifyUser("/UserLikeComment", obj);
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
                _log.Error(e);
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
                _log.Error(e);
                return false;
            }

            string reciverId = _usersRepository.GetPosting(postId).UserId;
            if (user.UserId != reciverId)
            {
                object obj = new { postId, user, ReciverId = reciverId };
                _serverComunication.NotifyUser("/UserLikePost", obj);
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
                _log.Error(e);
                return false;
            }
        }
    }
}
