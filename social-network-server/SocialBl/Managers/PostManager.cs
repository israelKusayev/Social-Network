using Social_Common.Interfaces.Managers;
using Social_Common.Interfaces.Repositories;
using Social_Common.Models;
using Social_Common.Models.Dtos;
using System;
using System.Collections.Generic;

namespace SocialBl.Managers
{
    public class PostManager : IPostManager
    {
        IAmazonS3Uploader _s3Uploader;
        IPostsRepository _postsRepository;
        public PostManager(IAmazonS3Uploader s3Uploader, 
            IPostsRepository postsRepository)
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
                imgUrl = postDto.Image == null ? null : _s3Uploader.UploadFile(postDto.Image, postId);

                Post post = new Post()
                {
                    Content = postDto.Content,
                    CreatedOn = DateTime.UtcNow,
                    PostId = postId,
                    Visability = postDto.WhoIsWatching,
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

        public PostListDto GetPosts(int start, int count, string userId)
        {
            return _postsRepository.GetFeed(start, count, userId);
        }
    }
}
