using Social_Common.Interfaces.Managers;
using Social_Common.Models;
using Social_Common.Models.Dtos;
using Social_Fe.Attributes;
using System.Linq;
using System.Web.Http;

namespace Social_Fe.Controllers
{
    public class PostController : ApiController
    {
        IPostManager _postManager;
        ITokenManager _tokenManager;
        public PostController(IPostManager postManager, ITokenManager tokenManager)
        {
            _postManager = postManager;
            _tokenManager = tokenManager;
        }
        [JWTAuth]
        [HttpPost]
        public IHttpActionResult CreatePost(CreatePostDto post)
        {
            if (string.IsNullOrWhiteSpace(post.Content))
            {
                return BadRequest("Content is required");
            }
            var token = Request.Headers.GetValues("x-auth-token").First();

            Post createdPost = _postManager.CreatePost(post, _tokenManager.GetUserId(token));
            if (createdPost != null)
            {
                return Ok();
            }
            return InternalServerError();
        }
    }
}
