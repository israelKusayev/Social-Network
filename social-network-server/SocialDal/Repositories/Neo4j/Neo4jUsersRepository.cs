using Social_Common.Models;
using Social_Common.Models.Dtos;
using Social_Common.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace SocialDal.Repositories.Neo4j
{
    public class Neo4jUsersRepository : Neo4jBaseRepository, IUsersRepository
    {
        public void Create(User user)
        {
            base.Create(user);
        }

        public List<User> Find(string name, string userId)
        {
            string query = $"MATCH (u:User) where u.UserName =~ '.*{name}.*' " +
               "AND NOT EXISTS((u)-[:Blocked]-(:User{UserId:'" + userId + "'}))" +
               "return u;";
            var res = Query(query);
            return RecordsToList<User>(res.ToList());
        }

        public User Get(string userId, string myId)
        {
            string query = "match(u:User{UserId:'" + userId + "'})" +
                "WHERE NOT EXISTS((u)-[:Blocked]-(:User{UserId:" + myId + "}))" +
                " return u";
            var res = Query(query);
            var user = res.Single();
            return user != null ? RecordToObj<User>(user) : null;
        }

        public void Block(string blockingUserId, string blockedUserId)
        {
            string query = "MATCH (blocking:User{UserId:'" + blockingUserId + "'})," +
                "(blocked:User{UserId:'" + blockedUserId + "'})" +
                "MERGE (blocking)-[r:Blocked]->(blocked)";
            Query(query);

            UnFollow(blockingUserId, blockedUserId);
            UnFollow(blockedUserId, blockingUserId);
        }

        public void UnBlock(string blockingUserId, string blockedUserId)
        {
            string query = "MATCH (:User{UserId:'" + blockingUserId + "'})-[r:Blocked]->" +
                "(:User{UserId:'" + blockedUserId + "'})" +
                "DELETE r";
            Query(query);
        }

        public void Follow(string followingUserId, string followedUserId)
        {
            string query = "MATCH (following:User{UserId:'" + followingUserId + "'})," +
                "(followed:User{UserId:'" + followedUserId + "'})" +
                "MERGE (following)-[r:Following]->(followed)" +
                "RETURN type(r)";
            Query(query);
        }

        public void UnFollow(string followingUserId, string followedUserId)
        {
            string query = "MATCH (:User{UserId:'" + followingUserId + "'})-[r:Following]->" +
                "(:User{UserId:'" + followedUserId + "'})" +
                "DELETE r";
            var res = Query(query);

        }

        public bool IsFollow(string userId, string followedUserId)
        {

            string query = " return exists((: User{ UserId:'" + userId + "'})-[:Following]->(:User{ UserId:'" + followedUserId + "'})) as isFollow";
            var res = Query(query);

            return (bool)res.Single()[0];
        }


        public List<User> GetFollowing(string userId)
        {
            string query = "match(:User{ UserId:'" + userId + "'})-[:Following]->(u: User) return u";
            var res = Query(query);
            return RecordsToList<User>(res.ToList());
        }

        public List<FollowersDTO> GetFollowers(string userId)
        {
            string query = "match (me:User{UserId:'" + userId + "'})<-[:Following]-(u:User) return u.UserName as UserName, u.UserId as UserId, exists((me)-[:Following]->(u)) as IsFollowing";
            var res = Query(query);
            return UnestedRecordToList<FollowersDTO>(res.ToList());
        }

        public List<User> GetBlockedUsers(string userId)
        {
            string query = "match(:User{ UserId:'" + userId + "'})-[:Blocked]->(u: User) return u";
            var res = Query(query);
            return RecordsToList<User>(res.ToList());
        }
    }
}
