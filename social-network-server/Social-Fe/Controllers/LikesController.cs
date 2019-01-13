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
        private TokenManager _tokenManager;

        public LikesController(LikesManager likesManager)
        {
            _likesManager = likesManager;
            _tokenManager = new TokenManager();
        }

        [HttpPost]
        [JWTAuth]
        [Route("api/Likes/LikePost/{postId}")]
        public IHttpActionResult LikePost(string postId)
        {
            var token = Request.Headers.GetValues("x-auth-token").First();
            string userId = _tokenManager.GetUserId(token);
            if (_likesManager.LikePost(userId, postId))
            {
                return Ok();
            }
            return InternalServerError();
        }

        [HttpPost]
        [JWTAuth]
        [Route("api/Likes/UnLikePost/{postId}")]
        public IHttpActionResult UnLikePost(string postId)
        {
            var token = Request.Headers.GetValues("x-auth-token").First();
            string userId = _tokenManager.GetUserId(token);
            if (_likesManager.UnLikePost(userId, postId))
            {
                return Ok();
            }
            return InternalServerError();
        }

        [HttpPost]
        [JWTAuth]
        [Route("api/Likes/LikeComment/{commentId}")]
        public IHttpActionResult LikeComment(string commentId)
        {
            var token = Request.Headers.GetValues("x-auth-token").First();
            string userId = _tokenManager.GetUserId(token);
            if (_likesManager.LikeComment(userId, commentId))
            {
                return Ok();
            }
            return InternalServerError();
        }

        [HttpPost]
        [JWTAuth]
        [Route("api/Likes/UnLikeComment/{commentId}")]
        public IHttpActionResult UnLikeComment(string commentId)
        {
            var token = Request.Headers.GetValues("x-auth-token").First();
            string userId = _tokenManager.GetUserId(token);
            if (_likesManager.UnLikeComment(userId, commentId))
            {
                return Ok();
            }
            return InternalServerError();
        }
    }
}
