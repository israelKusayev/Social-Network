using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Microsoft.Extensions.Configuration;
using Notification_Common.Interfaces.Repositories;
using System.Collections.Generic;

namespace Notification_Dal
{
    public class DynamoDbRepository<T> : IDynamoDbRepository<T>
    {
        private DynamoDBContext _DbContext;
        IConfiguration _configuration;

        public DynamoDbRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            var options = _configuration.GetAWSOptions();

            IAmazonDynamoDB DynamoClient = options.CreateServiceClient<IAmazonDynamoDB>();

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
            T savedItem = _DbContext.LoadAsync(record).Result;
            if (savedItem != null)
            {
                return false;
            }
            _DbContext.SaveAsync<T>(record);
            return true;
        }

        public bool Update(T record)
        {
            T savedItem = _DbContext.LoadAsync(record).Result;

            if (savedItem == null)
            {
                return false;
            }
            _DbContext.SaveAsync(record);
            return true;
        }

        public T Get<K>(K recordId)
        {
            return _DbContext.LoadAsync<T>(recordId).Result;
        }

        public List<T> GetAll<K>(K recordId)
        {
            DynamoDBOperationConfig config = new DynamoDBOperationConfig()
            {
                BackwardQuery = true,
            };
            var batch = _DbContext.ScanAsync<T>(new List<ScanCondition>() {
                  new ScanCondition("UserId", ScanOperator.Equal,recordId)
                }
           , config);
            return batch.GetNextSetAsync().Result;
        }
    }
}
