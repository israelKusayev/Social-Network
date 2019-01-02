using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Identity_Common.Interfaces.Repositories;

namespace Idetity_dal
{
    public class DynamoDbRepository<T> : IDynamoDbRepository<T>
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

        

        public bool Add(T record)
        {
            T savedItem = _DbContext.Load(record);
            if (savedItem != null)
            {
                return false;
            }
            _DbContext.Save<T>(record);
            return true;
        }

        public bool Update(T record)
        {
            T savedItem = _DbContext.Load(record);

            if (savedItem == null)
            {
                return false;
            }            
            _DbContext.Save(record);
            return true;
        }

        public T Get<K>(K recordId)
        {
            return _DbContext.Load<T>(recordId);
        }
    }
}
