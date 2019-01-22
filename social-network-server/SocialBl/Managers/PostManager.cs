using Social_Common.Interfaces.Helpers;
using Social_Common.Interfaces.Managers;
using Social_Common.Interfaces.Repositories;
using Social_Common.Models;
using Social_Common.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;

namespace SocialBl.Managers
{
    public class PostManager : IPostManager
    {
        private readonly IAmazonS3Uploader _s3Uploader;
        private readonly IPostsRepository _postsRepository;
        private readonly IServerComunication _serverComunication;

        public PostManager(IAmazonS3Uploader s3Uploader,
            IPostsRepository postsRepository, IServerComunication serverComunication)
        {
            _s3Uploader = s3Uploader;
            _postsRepository = postsRepository;
            _serverComunication = serverComunication;
        }

        public Post CreatePost(CreatePostDto postDto, User user)
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

                _postsRepository.Create(post, user.UserId);

                foreach (var reference in postDto.Referencing)
                {
                    _postsRepository.CreateReference(postId, reference.UserId, reference.StartIndex, reference.EndIndex);

                    if (user.UserId != reference.UserId)
                    {
                        object referencigNoification = new { user, postId, ReciverId = reference.UserId };
                        _serverComunication.NotifyUser("/ReferenceInPost", referencigNoification);
                    }
                }
                return post;
            }
            catch (Exception)
            {
                //todo logger
                return null;
            }
        }

        public ReturnedPostDto GetPost(string userId, string postId)
        {
            return _postsRepository.GetPost(userId, postId);
        }

        public PostListDto GetPosts(int start, int count, string userId)
        {
            return _postsRepository.GetFeed(start, count, userId);
        }
    }
}
