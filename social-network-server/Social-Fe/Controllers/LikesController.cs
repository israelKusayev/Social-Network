using Social_Common.Interfaces.Managers;
using Social_Fe.Attributes;
using System.Linq;
using System.Web.Http;

namespace Social_Fe.Controllers
{
    public class LikesController : ApiController
    {
        private readonly ILikesManager _likesManager;
        private readonly ITokenManager _tokenManager;
        

        public LikesController(ILikesManager likesManager, ITokenManager tokenManager)
        {
            _likesManager = likesManager;
            _tokenManager = tokenManager;
        }

        [HttpPost]
        [JWTAuth]
        [Route("api/Likes/LikePost/{postId}")]
        public IHttpActionResult LikePost(string postId)
        {
            var token = Request.Headers.GetValues("x-auth-token").First();

            if (_likesManager.LikePost(_tokenManager.GetUser(token), postId))
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
            if (_likesManager.LikeComment(_tokenManager.GetUser(token), commentId))
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
