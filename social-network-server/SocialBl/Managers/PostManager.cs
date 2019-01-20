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
        private string _notificationsUrl = ConfigurationManager.AppSettings["NotificationsServiceUrl"];

        private IAmazonS3Uploader _s3Uploader;
        private IPostsRepository _postsRepository;


        public PostManager(IAmazonS3Uploader s3Uploader,
            IPostsRepository postsRepository)
        {
            _s3Uploader = s3Uploader;
            _postsRepository = postsRepository;
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
                    using (var http = new HttpClient())
                    {
                        object referencigNoification = new { user, postId, ReciverId = reference.UserId };
                        var response = http.PostAsJsonAsync(_notificationsUrl + "/ReferenceInPost", referencigNoification).Result;
                        if (!response.IsSuccessStatusCode)
                        {
                            // todo logger
                        }
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
            return _postsRepository.getPost(userId, postId);
        }

        public PostListDto GetPosts(int start, int count, string userId)
        {
            return _postsRepository.GetFeed(start, count, userId);
        }
    }
}
