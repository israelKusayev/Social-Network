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

        public ReturnedPostDto Get(string postId, string userId)
        {
            string query = "match(p:Post{PostId:'" + postId + "'})-[:PostedBy]->(posting:User)," +
                "(p)<-[l:Like]-(:User), (p)-[:Referencing]->(ref:User), (me:User{UserId:'" + userId + "'})" +
                $"WHERE NOT (posting)-[:Blocked]-(me) AND" +
                $" (p.Visability=={PostVisabilityOptions.All.ToString()} OR " +
                $"(p.Visability=={PostVisabilityOptions.Followers.ToString()} AND EXISTS( (me)-[:Following]->(posting) )) )" +
                "return p, posting AS User ," +
                "EXISTS( (p)<-[:Like]-(me) ) AS IsLiked," +
                "COUNT(l) AS Likes, COLLECT(ref) AS Referencing";
            var res = Query(query);
            var post = res.Single();
            return post != null ? RecordToObj<ReturnedPostDto>(post) : null;
        }

        public void Create(Post post, string postedByUserId)
        {
            Create(post);

            string postedByQuery = $@"MATCH (p:Post),(u:User)
                WHERE p.PostId = '{post.PostId}' AND u.UserId = '{postedByUserId}'
                CREATE(p) -[r:PostedBy]->(u)
                RETURN type(r)";
            Query(postedByQuery);
        }

        public PostListDto GetFeed(int startIdx, int count, string userId)
        {
            if (count > _maxPostsPerPage)
                count = _maxPostsPerPage;
            string query = "MATCH(p: Post) -[:PostedBy]->(posting: User), " +
                    "(me: User{ UserId: '"+userId+"'}) " +
                "OPTIONAL MATCH(p)< -[l: Like] - (: User) " +
                "OPTIONAL MATCH(p)-[:Referencing]->(ref:User)" +
                "WHERE NOT EXISTS((posting) -[:Blocked] - (me)) " +
                    "AND(p.Visability = 0 OR (p.Visability = 1 " +
                    "AND EXISTS((me) -[:Following]->(posting)))) " +
                    "AND(EXISTS((p) -[:Recomended]->(me)) " +
                        "OR EXISTS((p) -[:Referencing]->(me)) " +
                        "OR EXISTS((p) < -[:CommentedOn] - (: Comment) -[:Referencing]->(me)) " +
                        "OR EXISTS((me) - [:Following]->(posting))) " +
                "RETURN " +
                    "p AS Post, posting AS CreatedBy, " +
                    "EXISTS( (p) < -[:Like]-(me) ) AS IsLiked, " +
                    "COUNT(l) AS Likes, COLLECT(ref) AS Referencing " +
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
                ReturnedPostDto dto = new ReturnedPostDto();
                FlattenPost(record, dto);
                dto.CreatedBy = ExtractUser(record);
                dto.IsLiked = (bool)record["IsLiked"];
                dto.Likes = (int)(long)record["Likes"];
                list.Add(dto);
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
            dto.CreatedOn = DateTime.Parse((string)postProps[nameof(dto.CreatedOn)]);
            dto.ImgUrl = postProps.ContainsKey(nameof(dto.ImgUrl)) ? (string)postProps[nameof(dto.ImgUrl)] : null;
            dto.PostId = (string)postProps[nameof(dto.PostId)];
            dto.Visability = (PostVisabilityOptions)((long)postProps[nameof(dto.Visability)]);
        }
    }
}
