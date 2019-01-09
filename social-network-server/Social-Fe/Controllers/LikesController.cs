using Social_Common.Models.Dtos;
using Social_Fe.Attributes;
using SocialBl.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Social_Fe.Controllers
{
    public class LikesController : ApiController
    {
        private LikesManager _likesManager;

        public LikesController()
        {
            _likesManager = new LikesManager();
        }

        [HttpPost]
        [JWTAuth]
        [Route("/api/Likes/LikePost")]
        public IHttpActionResult LikePost(LikeDto dto)
        {
            if(_likesManager.LikePost(dto))
            {
                return Ok();
            }
            return InternalServerError();
        }

        [HttpPost]
        [JWTAuth]
        [Route("/api/Likes/UnLikePost")]
        public IHttpActionResult UnLikePost(LikeDto dto)
        {
            if (_likesManager.UnLikePost(dto))
            {
                return Ok();
            }
            return InternalServerError();
        }

        [HttpPost]
        [JWTAuth]
        [Route("/api/Likes/LikeComment")]
        public IHttpActionResult LikeComment(LikeDto dto)
        {
            if (_likesManager.LikeComment(dto))
            {
                return Ok();
            }
            return InternalServerError();
        }

        [HttpPost]
        [JWTAuth]
        [Route("/api/Likes/UnLikeComment")]
        public IHttpActionResult UnLikeComment(LikeDto dto)
        {
            if (_likesManager.UnLikeComment(dto))
            {
                return Ok();
            }
            return InternalServerError();
        }
    }
}
