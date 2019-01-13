using Social_Common.Interfaces.Managers;
using Social_Common.Interfaces.Repositories;
using Social_Common.Models.Dtos;
using System;

namespace SocialBl.Managers
{
    public class LikesManager : ILikesManager
    {
        private ILikesRepository _likesRepository;

        public LikesManager(ILikesRepository likesRepository)
        {
            _likesRepository = likesRepository;
        }

        public bool LikeComment(LikeDto dto)
        {
            try
            {
                _likesRepository.LikeComment(dto.UserId, dto.ItemId);
                return true;
            }
            catch (Exception e)
            {
                //TODO add log here
                return false;
            }
        }

        public bool UnLikeComment(LikeDto dto)
        {
            try
            {
                _likesRepository.UnLikeComment(dto.UserId, dto.ItemId);
                return true;
            }
            catch (Exception e)
            {
                //TODO add log here
                return false;
            }
        }

        public bool LikePost(LikeDto dto)
        {
            try
            {
                _likesRepository.LikePost(dto.UserId, dto.ItemId);
                return true;
            }
            catch (Exception e)
            {
                //TODO add log here
                return false;
            }
        }

        public bool UnLikePost(LikeDto dto)
        {
            try
            {
                _likesRepository.UnLikePost(dto.UserId, dto.ItemId);
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
