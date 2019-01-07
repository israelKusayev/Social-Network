using Newtonsoft.Json;
using Social_Common.Models;
using Social_Common.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialDal.Repositories.Neo4j
{
    public class Neo4jPostsRepository : Neo4jBaseRepository
    {
        public ReturnedPostDto Get(string postId,string userId)
        {
            string query = @"match(p:Post{PostId:'" + postId + "'})-[:PostedBy]->(User:User)," +
                "(p)<-[l:Like]-(u:User)" +
                "return p, u AS User ," +
                "EXISTS( (p)<-[:Like]-(me:User{UserId:'" + userId + "'}) ) AS IsLiked,";
            var res = Query(query);
            var post = res.Single();
            return post != null ? RecordToObject<ReturnedPostDto>(post) : null;
        }

        public void Create(Post post,string postedByUserId)
        {
            Create(post);
            string postedByQuery = $@"MATCH (a:Post),(b:User)
                WHERE a.PostId = '{post.PostId}' AND UserId = '{postedByUserId}'
                CREATE(a) -[r: PostedBy]->(b)
                RETURN type(r)";
            Query(postedByQuery);
        }


    }
}
