using Newtonsoft.Json;
using Social_Common.Enum;
using Social_Common.Models;
using Social_Common.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialDal.Repositories.Neo4j
{
    public class Neo4jPostsRepository : Neo4jBaseRepository
    {
        private static int _postsPerPage = int.Parse(ConfigurationManager.AppSettings["PostsPerPage"]);

        public ReturnedPostDto Get(string postId, string userId)
        {
            string query = "match(p:Post{PostId:'" + postId + "'})-[:PostedBy]->(posting:User)," +
                "(p)<-[l:Like]-(:User), (p)-[:Referencing]->(ref:User), (me:User{UserId:'" + userId + "'})" +
                $"WHERE NOT (posting)-[:Blocked]-(me) AND" +
                $" (p.Visability=={PostVisabilityOptions.All.ToString()} OR " +
                $"(p.Visability=={PostVisabilityOptions.Followers.ToString()} AND EXISTS( (me)-[:Like]->(posting) )) )" +
                "return p, posting AS User ," +
                "EXISTS( (p)<-[:Like]-(me) ) AS IsLiked," +
                "COUNT(l) AS Likes, COLLECT(ref) AS Referencing";
            var res = Query(query);
            var post = res.Single();
            return post != null ? RecordToObject<ReturnedPostDto>(post) : null;
        }

        public void Create(Post post, string postedByUserId)
        {
            Create(post);
            string postedByQuery = $@"MATCH (a:Post),(b:User)
                WHERE a.PostId = '{post.PostId}' AND UserId = '{postedByUserId}'
                CREATE(a) -[r: PostedBy]->(b)
                RETURN type(r)";
            Query(postedByQuery);
        }

        public PostListDto GetFeed(int page, string userId)
        {
            int start = page * _postsPerPage;
            string query = "match(p:Post)-[:PostedBy]->(posting:User)," +
                "(p)<-[l:Like]-(:User), (p)-[:Referencing]->(ref:User)," +
                " (me:User{UserId:'" + userId + "'})" +
                "WHERE NOT (posting)-[:Blocked]-(me) AND" +
                $" (p.Visability=={PostVisabilityOptions.All.ToString()} OR " +
                $"(p.Visability=={PostVisabilityOptions.Followers.ToString()} AND EXISTS( (me)-[:Like]->(posting) )) )" +
                "AND (EXISTS((p)-[:Recomended]->(me)) OR EXSITS((p)-[:Referencing]->(me)) " +
                "OR EXSISTS ((p)<-[:CommentedOn]-(:Comment)-[:Referencing]->(me)) )" +
                "return p, posting AS User ," +
                "EXISTS( (p)<-[:Like]-(me) ) AS IsLiked," +
                "COUNT(l) AS Likes, COLLECT(ref) AS Referencing" +
                $"ORDER BY p.CreatedOn DESC SKIP {start} LIMIT {_postsPerPage}";
            var res = Query(query);
            PostListDto postListDto = new PostListDto()
            {
                Page = page,
                Posts = RecordsToList<ReturnedPostDto>(res)
            };
            return postListDto;
        }
    }
}
