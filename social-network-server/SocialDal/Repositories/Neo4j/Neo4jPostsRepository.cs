using Neo4j.Driver.V1;
using Social_Common.Enum;
using Social_Common.Interfaces.Repositories;
using Social_Common.Models;
using Social_Common.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace SocialDal.Repositories.Neo4j
{
    public class Neo4jPostsRepository : Neo4jBaseRepository, IPostsRepository
    {
        private static int _maxPostsPerPage = int.Parse(ConfigurationManager.AppSettings["MaxPostsPerPage"]);

        public void Create(Post post, string postedByUserId)
        {
            Create(post);

            string postedByQuery = $@"MATCH (p:Post),(u:User)
                WHERE p.PostId = '{post.PostId}' AND u.UserId = '{postedByUserId}'
                CREATE(p) -[r:PostedBy]->(u)
                RETURN type(r)";
            Query(postedByQuery);
        }

        public void CreateReference(string postId, string userId, int startIdx, int endIdx)
        {
            string query = "MATCH (p:Post{PostId:'" + postId + "'}), (u:User{UserId:'" + userId + "'}) " +
                "CREATE (p)-[:Referencing{StartIndex:" + startIdx + ",EndIndex:" + endIdx + "}]->(u)";
            Query(query);
        }

        public PostListDto GetFeed(int startIdx, int count, string userId)
        {
            if (count > _maxPostsPerPage)
                count = _maxPostsPerPage;
            string query = "MATCH(p: Post) -[:PostedBy]->(posting: User), " +
                    "(me: User{ UserId: '" + userId + "'}) " +
                "WHERE NOT EXISTS((posting) -[:Blocked] - (me)) " +
                    "AND(me.UserId = posting.UserId OR " +
                    "p.Visability = 0 OR (p.Visability = 1 " +
                    "AND EXISTS((me) -[:Following]->(posting)))) " +
                    "AND(EXISTS((p) -[:Recomended]->(me)) " +
                        "OR EXISTS((p) -[:Referencing]->(me)) " +
                        "OR EXISTS((p) < -[:CommentedOn] - (: Comment) -[:Referencing]->(me)) " +
                        "OR EXISTS((me) - [:Following]->(posting)) " +
                        "OR me.UserId = posting.UserId ) " +
                "OPTIONAL MATCH(p)< -[l: Like] - (: User) " +
                "OPTIONAL MATCH(p)-[rel:Referencing]->(ref:User)" +
                "RETURN " +
                    "p AS Post, posting AS CreatedBy, " +
                    "EXISTS( (p) < -[:Like]-(me) ) AS IsLiked, " +
                    "COUNT(l) AS Likes, COLLECT({rel:rel,user:ref}) AS Referencing " +
                $"ORDER BY p.CreatedOn DESC SKIP {startIdx} LIMIT {count}";
            var res = Query(query);
            PostListDto postListDto = new PostListDto()
            {
                Posts = DeserializeFeed(res)
            };
            return postListDto;
        }

        private static List<ReturnedPostDto> DeserializeFeed(IEnumerable<IRecord> records)
        {
            var list = new List<ReturnedPostDto>();
            foreach (IRecord record in records)
            {
                list.Add(DeserializePost(record));
            }
            return list;
        }

        private static ReturnedPostDto DeserializePost(IRecord record)
        {
            ReturnedPostDto dto = new ReturnedPostDto();
            FlattenPost(record, dto);
            dto.CreatedBy = ExtractUser(record);
            dto.IsLiked = (bool)record["IsLiked"];
            dto.Likes = (int)(long)record["Likes"];
            dto.Referencing = ExtractRefences(record);
            return dto;
        }

        private static List<ReferencingDto> ExtractRefences(IRecord record)
        {
            var list = new List<ReferencingDto>();
            List<object> references = (List<object>)record["Referencing"];
            foreach (var reference in references)
            {
                var props = (Dictionary<string, object>)reference;
                ReferencingDto dto = new ReferencingDto();
                if (props["user"] != null && props["rel"] != null)
                {
                    var userProps = (Dictionary<string, object>)props["user"].GetType().GetProperty("Properties").GetValue(props["user"]);
                    var relProps = (Dictionary<string, object>)props["rel"].GetType().GetProperty("Properties").GetValue(props["rel"]);
                    dto.StartIndex = (int)(long)relProps[nameof(dto.StartIndex)];
                    dto.EndIndex = (int)(long)relProps[nameof(dto.EndIndex)];
                    dto.UserId = (string)userProps[nameof(dto.UserId)];
                    dto.UserName = (string)userProps[nameof(dto.UserName)];
                    list.Add(dto);
                }
            }
            return list;
        }

        private static User ExtractUser(IRecord record)
        {
            var dynamicUser = record["CreatedBy"];
            User user = new User();
            var userProps = (Dictionary<string, object>)dynamicUser.GetType().GetProperty("Properties").GetValue(dynamicUser);
            user.UserId = (string)userProps[nameof(user.UserId)];
            user.UserName = (string)userProps[nameof(user.UserName)];
            return user;
        }

        private static void FlattenPost(IRecord record, ReturnedPostDto dto)
        {
            var dynamicPost = record["Post"];
            var postProps = (Dictionary<string, object>)dynamicPost.GetType().GetProperty("Properties").GetValue(dynamicPost);
            dto.Content = (string)postProps[nameof(dto.Content)];
            dto.CreatedOn = DateTime.Parse((string)postProps[nameof(dto.CreatedOn)]).ToUniversalTime();
            dto.ImgUrl = postProps.ContainsKey(nameof(dto.ImgUrl)) ? (string)postProps[nameof(dto.ImgUrl)] : null;
            dto.PostId = (string)postProps[nameof(dto.PostId)];
            dto.Visability = (PostVisabilityOptions)((long)postProps[nameof(dto.Visability)]);
        }

        public ReturnedPostDto getPost(string userId, string postId)
        {
            string query = "MATCH(p: Post{PostId:'" + postId + "'}) -[:PostedBy]->(posting: User), " +
                    "(me: User{ UserId: '" + userId + "'}) " +
                "WHERE NOT EXISTS((posting) -[:Blocked] - (me))   " +
                "OPTIONAL MATCH(p)< -[l: Like] - (: User) " +
                "OPTIONAL MATCH(p)-[rel:Referencing]->(ref:User) " +
                "RETURN " +
                    "p AS Post, posting AS CreatedBy, " +
                    "EXISTS( (p) < -[:Like]-(me) ) AS IsLiked, " +
                    "COUNT(l) AS Likes, COLLECT({rel:rel,user:ref}) AS Referencing ";
            var res = Query(query);
            var post = DeserializePost(res.Single());
            return post;
        }

        public string GetPostIdByCommentId(string commentId)
        {
            string query = "MATCH(: Comment{CommentId:'" + commentId + "'}) -[:CommentedOn]->(p: Post) return p.PostId";
            var res = Query(query);
            return (string)res.Single()[0];

        }
    }
}
