using Social_Common.Enum;
using Social_Common.Interfaces.Repositories;
using Social_Common.Models;
using Social_Common.Models.Dtos;
using System.Collections.Generic;

namespace SocialDal.Repositories.Neo4j
{
    public class Neo4jCommentsRepository : Neo4jBaseRepository, ICommentsRepository
    {

        public void Create(Comment comment, string userId, string postId)
        {
            Create(comment);
            string CommentedByQuery = $@"MATCH (c:Comment),(u:User)
                WHERE c.CommentId = '{comment.CommentId}' AND u.UserId = '{userId}'
                CREATE(c) -[r:CommentedBy]->(u)";
            Query(CommentedByQuery);
            string CommentedOnQuery = $@"MATCH (c:Comment),(p:Post)
                WHERE c.CommentId = '{comment.CommentId}' AND p.PostId = '{postId}'
                CREATE(c) -[r:CommentedOn]->(p)";
            Query(CommentedOnQuery);
        }

        public List<ReturnedCommentDto> GetComments(string userId, string postId)
        {
            string query = "MATCH (c:Comment)-[:CommentedOn]->(p:Post{PostId:'" + postId + "'})-[:PostedBy]->(posting:User)," +
                "(c)-[:CommentedBy]->(u:User),(c)-[:Referencing]->(ref:User)," +
                "(me:User{UserId:'" + userId + "'})" +
                "WHERE NOT EXSISTS((posting)-[:Blocked]-(me)) AND" +
                $" (p.Visability=={(int)PostVisabilityOptions.All} OR " +
                $"(p.Visability=={(int)PostVisabilityOptions.Followers} AND EXISTS( (me)-[:Following]->(posting) )) )";
            var res = Query(query);
            return RecordsToList<ReturnedCommentDto>(res);
        }

    }
}
