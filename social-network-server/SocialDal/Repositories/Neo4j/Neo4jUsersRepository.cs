using Social_Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialDal.Repositories.Neo4j
{
    public class Neo4jUsersRepository: Neo4jBaseRepository
    {
        public void Create(User user)
        {
          base.Create(user);
        }

        public List<User> Find(string name)
        {
            string query = $"MATCH (u:User) where u.UserName =~ '.*{name}.*' return u;";
            var res = Query(query);
            //var r = res.ToList();
            return RecordsToList<User>(res.ToList());
        }

        public User Get(string userId)
        {
            string query = "match(u:User{UserId:'"+userId+"'}) return u";
            var res = Query(query);
            var user = res.Single();
            return user!=null ? RecordToObject<User>(user) : null;
        }
    }
}
