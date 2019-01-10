using Social_Common.Enum;
using Social_Common.Interfaces.Repositories;
using Social_Common.Models;
using Social_Common.Models.Dtos;
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
            string query = "match(p:Post)-[:PostedBy]->(posting:User)," +
                "(p)<-[l:Like]-(:User), (p)-[:Referencing]->(ref:User)," +
                " (me:User{UserId:'" + userId + "'})" +
                "WHERE NOT EXSISTS((posting)-[:Blocked]-(me)) AND" +
                $" (p.Visability=={(int)PostVisabilityOptions.All} OR " +
                $"(p.Visability=={(int)PostVisabilityOptions.Followers} AND EXISTS( (me)-[:Following]->(posting) )) )" +
                "AND (EXISTS((p)-[:Recomended]->(me)) OR EXSITS((p)-[:Referencing]->(me)) " +
                "OR EXSISTS ((p)<-[:CommentedOn]-(:Comment)-[:Referencing]->(me)) )" +
                "return p, posting AS User ," +
                "EXISTS( (p)<-[:Like]-(me) ) AS IsLiked," +
                "COUNT(l) AS Likes, COLLECT(ref) AS Referencing" +
                $"ORDER BY p.CreatedOn DESC SKIP {startIdx} LIMIT {count}";
            var res = Query(query);
            PostListDto postListDto = new PostListDto()
            {
                Posts = RecordsToList<ReturnedPostDto>(res)
            };
            return postListDto;
        }
    }
}
