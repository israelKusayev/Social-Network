using Social_Common.Interfaces.Managers;
using Social_Common.Models;
using Social_Common.Models.Dtos;
using SocialDal.Repositories.Neo4j;
using System;

namespace SocialBl.Managers
{
    public class PostManager : IPostManager
    {
        AmazonS3Uploader _s3Uploader;
        Neo4jPostsRepository _postsRepository;
        public PostManager(AmazonS3Uploader s3Uploader, 
            Neo4jPostsRepository postsRepository)
        {
            _s3Uploader = s3Uploader;
            _postsRepository = postsRepository;
        }

        public Post CreatePost(CreatePostDto postDto, string userId)
        {
            string postId = Guid.NewGuid().ToString();

            string imgUrl = null;
            try
            {
                imgUrl = imgUrl == null ? null : _s3Uploader.UploadFile(postDto.Image, postId);

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
