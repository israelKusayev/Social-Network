using Social_Common.Models;
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
    public class PostsController : ApiController
    {
        PostManager _postManager;
        TokenManager _tokenManager;
        public PostsController(PostManager postManager, TokenManager tokenManager)
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


        [JWTAuth]
        [HttpGet]
        [Route("api/post/{start}/{count}")]
        public IHttpActionResult GetPosts(int start, int count)
        {
            try
            {
                var token = Request.Headers.GetValues("x-auth-token").First();
                var userId = _tokenManager.GetUserId(token);
                PostListDto posts = _postManager.GetPosts(start, count, userId);
                return Ok(posts);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
    }
}
