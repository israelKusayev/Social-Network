using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Identity_Common.models;
using Identity_Common.interfaces;

namespace Idetity_dal
{
    class UsersIdentityRepository : IUsersIdentityRepository
    {

        private DynamoDBContext _DbContext;

        public UsersIdentityRepository()
        {
            var DynamoClient = new AmazonDynamoDBClient();
            _DbContext = new DynamoDBContext(DynamoClient, new DynamoDBContextConfig
            {
                //Setting the Consistent property to true ensures that you'll always get the latest 
                ConsistentRead = true,
                SkipVersionCheck = true
            });
        }

        ~UsersIdentityRepository()
        {
            _DbContext.Dispose();
        }

        

        public bool Add(User user)
        {
            User savedItem = _DbContext.Load(user);
            if (savedItem != null)
            {
                return false;
            }
            _DbContext.Save<User>(user);
            return true;
        }

        public bool Update(User user)
        {
            User savedItem = _DbContext.Load(user);

            if (savedItem == null)
            {
                return false;
            }            
            _DbContext.Save(user);
            return true;
        }

        public User Get(string userId)
        {
            return _DbContext.Load<User>(userId);
        }
    }
}
