using Social_Common.Models.Dtos;
using SocialDal.Repositories.Neo4j;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialBl.Managers
{
    public class LikesManager
    {
        private Neo4jLikesRepository _likesRepository;

        public LikesManager()
        {
            _likesRepository = new Neo4jLikesRepository();
        }

        public bool LikeComment(LikeDto dto)
        {
            try
            {
                _likesRepository.LikeComment(dto.UserId, dto.ItemId);
                return true;
            }
            catch(Exception e)
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
