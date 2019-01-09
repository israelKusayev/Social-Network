using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialDal.Repositories.Neo4j
{
    public class Neo4jLikesRepository : Neo4jBaseRepository
    {

        protected void Like(string userId, string objectId, string objectType)
        {
            string query = "MATCH (u:User{UserId:" + userId + "})," +
                "(o:"+objectType+"{"+objectType+"Id:" + objectId + "})" +
                "CREATE (u)-[r:Like]->(o)" +
                "RETURN type(r)";
        }

        protected void UnLike(string userId, string objectId, string objectType)
        {
            string query = "MATCH (:User{UserId:" + userId + "})-[r:Like]->" +
                "(:"+objectType+"{"+objectType+"Id:" + objectId + "})" +
                "DELETE r";
        }

        public void LikePost(string userId, string PostId)
        {
            Like(userId, PostId, "Post");
        }

        public void UnLikePost(string userId, string PostId)
        {
            UnLike(userId, PostId, "Post");
        }

        public void LikeComment(string userId, string CommentId)
        {
            Like(userId, CommentId, "Comment");
        }

        public void UnLikeComment(string userId, string CommentId)
        {
            UnLike(userId, CommentId, "Comment");
        }
    }
}
