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
    public class PostController : ApiController
    {
        PostManager _postManager;
        TokenManager _tokenManager;
        public PostController()
        {
            _postManager = new PostManager();
            _tokenManager = new TokenManager();
        }
        //[JWTAuth]
        [HttpPost]
        public IHttpActionResult CreatePost(CreatePostDto post)
        {
            if (string.IsNullOrWhiteSpace(post.Content))
            {
                return BadRequest("Content is required");
            }
            var token = Request.Headers.GetValues("x-auth-token").First();
            
            Post createdPost = _postManager.CreatePost(post, _tokenManager.GetUserId(token));
            return Ok();
        }
    }
}
