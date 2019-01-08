using Social_Common.Models;
using Social_Common.Models.Dtos;
using SocialDal.Repositories.Neo4j;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialBl.Managers
{
    public class PostManager
    {
        AmazonS3Uploader s3;
        Neo4jPostsRepository _postsRepository;
        public PostManager()
        {
            s3 = new AmazonS3Uploader();
            _postsRepository = new Neo4jPostsRepository();
        }

        public Post CreatePost(CreatePostDto postDto, string userId)
        {
            string postId = Guid.NewGuid().ToString();

            string imgUrl = null;
            try
            {
                imgUrl = imgUrl == null ? null : s3.UploadFile(postDto.Image, postId);

                Post post = new Post()
                {
                    Content = postDto.Content,
                    CreatedOn = DateTime.UtcNow,
                    PostId = postId,
                    Visability = postDto.WhoCanWatching,
                    ImgUrl = imgUrl
                };

                _postsRepository.Create(post, userId);
                return post;
            }
            catch (Exception)
            {
                //todo logger
                return null;
            }
        }
    }
}
