using Social_Common.Interfaces.Managers;
using Social_Common.Models.Dtos;
using SocialDal.Repositories.Neo4j;
using System;

namespace SocialBl.Managers
{
    public class LikesManager : ILikesManager
    {
        private Neo4jLikesRepository _likesRepository;

        public LikesManager(Neo4jLikesRepository likesRepository)
        {
            _likesRepository = likesRepository;
        }

        public bool LikeComment(string userId, string commentId)
        {
            try
            {
                _likesRepository.LikeComment(userId, commentId);
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

        public bool LikePost(string userId, string postId)
        {
            try
            {
                _likesRepository.LikePost(userId, postId);
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
                return false;
            }
            catch (Exception e)
            {
                //TODO add log here
                return false;
            }
        }
    }
}
