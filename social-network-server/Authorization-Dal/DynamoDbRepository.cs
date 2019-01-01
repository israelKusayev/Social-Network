using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Authorization_Common.Models;

namespace Authorization_Dal
{
    public class DynamoDbRepository<T>
    {
        private DynamoDBContext _DbContext;

        public DynamoDbRepository()
        {
            var DynamoClient = new AmazonDynamoDBClient();
            _DbContext = new DynamoDBContext(DynamoClient, new DynamoDBContextConfig
            {
                //Setting the Consistent property to true ensures that you'll always get the latest 
                ConsistentRead = true,
                SkipVersionCheck = true
            });
        }

        ~DynamoDbRepository()
        {
            _DbContext.Dispose();
        }



        public T Add(T user)
        {
            T savedItem = _DbContext.Load(user);
            if (savedItem != null)
            {
                return default(T);
            }
            _DbContext.Save<T>(user);
            return _DbContext.Load(user);
        }

        public T Update(T user)
        {
            T savedItem = _DbContext.Load(user);

            if (savedItem == null)
            {
                return default(T);
            }
            _DbContext.Save(user);
            return _DbContext.Load(user);
        }

        public T Get(string userId)
        {
            return _DbContext.Load<T>(userId);
        }
    }
}