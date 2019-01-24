using Neo4j.Driver.V1;
using Social_Common.Enum;
using Social_Common.Interfaces.Repositories;
using Social_Common.Models;
using Social_Common.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public void CreateReference(string commentId, string userId, int startIdx, int endIdx)
        {
            string query = "MATCH (c:Comment{CommentId:'" + commentId + "'}), (u:User{UserId:'" + userId + "'}) " +
                "CREATE (c)-[:Referencing{StartIndex:" + startIdx + ",EndIndex:" + endIdx + "}]->(u)";
            Query(query);
        }

        public List<ReturnedCommentDto> GetComments(string userId, string postId)
        {
            string query = "MATCH (c:Comment)-[:CommentedOn]->(p:Post{PostId:'" + postId + "'})-[:PostedBy]->(posting:User), " +
                "(c)-[:CommentedBy]->(u:User), " +
                "(me:User{UserId:'" + userId + "'})" +
                "WHERE NOT EXISTS((posting)-[:Blocked]-(me)) AND " +
                "NOT EXISTS((u)-[:Blocked]-(me)) AND" +
                $" (p.Visability={(int)PostVisabilityOptions.All} OR me.UserId = posting.UserId OR " +
                $"(p.Visability={(int)PostVisabilityOptions.Followers} AND EXISTS( (me)-[:Following]->(posting) )) )" +
                "OPTIONAL MATCH(c)-[rel:Referencing]->(ref:User) " +
                "OPTIONAL MATCH(c)<-[l:Like]- (: User) " +
                "RETURN c AS Comment, u AS CreatedBy, p.PostId AS PostId, " +
                "EXISTS( (c) <-[:Like]-(me) ) AS IsLiked, " +
                "COUNT(DISTINCT l) AS Likes, COLLECT({rel:rel,user:ref}) AS Referencing " +
                "ORDER BY c.CreatedOn DESC";
            var res = Query(query);
            return DeserializeComments(res);
        }

        private static List<ReturnedCommentDto> DeserializeComments(IEnumerable<IRecord> records)
        {
            var list = new List<ReturnedCommentDto>();
            foreach (IRecord record in records)
            {
                ReturnedCommentDto dto = new ReturnedCommentDto();
                FlattenComment(record, dto);
                dto.CreatedBy = ExtractUser(record);
                dto.IsLiked = (bool)record["IsLiked"];
                dto.Likes = (int)(long)record["Likes"];
                dto.PostId = (string)record["PostId"];
                dto.Referencing = ExtractRefences(record);
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

        private static void FlattenComment(IRecord record, ReturnedCommentDto dto)
        {
            var dynamicComment = record["Comment"];
            var commentProps = (Dictionary<string, object>)dynamicComment.GetType().GetProperty("Properties").GetValue(dynamicComment);
            dto.Content = (string)commentProps[nameof(dto.Content)];
            dto.CreatedOn = DateTime.Parse((string)commentProps[nameof(dto.CreatedOn)]).ToUniversalTime();
            dto.ImgUrl = commentProps.ContainsKey(nameof(dto.ImgUrl)) ? (string)commentProps[nameof(dto.ImgUrl)] : null;
            dto.CommentId = (string)commentProps[nameof(dto.CommentId)];
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
    }
}
