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
            Neo4jLikesRepository _likesRepository = new Neo4jLikesRepository();
        }

        public void LikeComment(LikeDto dto)
        {
            _likesRepository.LikeComment(dto.UserId, dto.ItemId);
        }

        public void UnLikeComment(LikeDto dto)
        {
            _likesRepository.UnLikeComment(dto.UserId, dto.ItemId);
        }

        public void LikePost(LikeDto dto)
        {
            _likesRepository.LikePost(dto.UserId, dto.ItemId);
        }

        public void UnLikePost(LikeDto dto)
        {
            _likesRepository.UnLikePost(dto.UserId, dto.ItemId);
        }
    }
}
