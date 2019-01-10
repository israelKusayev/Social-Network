using Social_Common.Models.Dtos;
using Social_Fe.Attributes;
using SocialBl.Managers;
using System.Linq;
using System.Web.Http;

namespace Social_Fe.Controllers
{
    public class CommentsController : ApiController
    {
        private CommentsManager _commentsManager;
        private TokenManager _tokenManager;

        public CommentsController()
        {
            _commentsManager = new CommentsManager();
            _tokenManager = new TokenManager();
        }

        [HttpPost]
        [JWTAuth]
        public IHttpActionResult Create([FromBody] CreateCommentDto commentDto)
        {
            var token = Request.Headers.GetValues("x-auth-token").First();
            string userId = _tokenManager.GetUserId(token);
            var res = _commentsManager.Create(commentDto, userId);
            if (res)
            {
                return Ok();
            }
            else
            {
                return InternalServerError();
            }
        }


        [HttpGet]
        [JWTAuth]
        public IHttpActionResult Get(string id)
        {
            var token = Request.Headers.GetValues("x-auth-token").First();
            string userId = _tokenManager.GetUserId(token);
            var res = _commentsManager.GetByPost(id, userId);
            if (res != null)
            {
                return Ok(res);
            }
            else
            {
                return InternalServerError();
            }
        }
    }
}
