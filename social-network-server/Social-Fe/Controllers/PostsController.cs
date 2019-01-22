using Social_Common.Interfaces.Managers;
using Social_Common.Models;
using Social_Common.Models.Dtos;
using Social_Fe.Attributes;
using System;
using System.Linq;
using System.Web.Http;

namespace Social_Fe.Controllers
{
    public class PostsController : ApiController
    {
        IPostManager _postManager;
        ITokenManager _tokenManager;
        public PostsController(IPostManager postManager, ITokenManager tokenManager)
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

            Post createdPost = _postManager.CreatePost(post, _tokenManager.GetUser(token));
            if (createdPost != null)
            {
                return Ok();
            }
            return InternalServerError();
        }

        [JWTAuth]
        [HttpGet]
        [Route("api/posts/{postId}")]
        public IHttpActionResult GetPosts(string postId)
        {
            try
            {
                var token = Request.Headers.GetValues("x-auth-token").First();
                var userId = _tokenManager.GetUserId(token);
                ReturnedPostDto post = _postManager.GetPost(userId, postId);
                return Ok(post);
            }
            catch (Exception e)
            {
                return InternalServerError();
            }
        }


        [JWTAuth]
        [HttpGet]
        [Route("api/posts/{start}/{count}")]
        public IHttpActionResult GetPosts(int start, int count)
        {
            try
            {
                var token = Request.Headers.GetValues("x-auth-token").First();
                var userId = _tokenManager.GetUserId(token);
                PostListDto posts = _postManager.GetPosts(start, count, userId);
                return Ok(posts);
            }
            catch (Exception e)
            {
                return InternalServerError();
            }
        }
    }
}
