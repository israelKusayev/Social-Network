using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;
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
            //AmazonDynamoDBClient client = new AmazonDynamoDBClient(new AmazonDynamoDBConfig(){user);
            //var request = new BatchGetItemRequest()
            //{
            //    RequestItems = new Dictionary<string, KeysAndAttributes>()
            //    {{
            //        "Notifications", new KeysAndAttributes()
            //        {
            //            Keys= new List<Dictionary<string, AttributeValue>>()
            //            {
            //                new Dictionary<string, AttributeValue>()
            //                {
            //                    { "UserId", new AttributeValue {S = recordId.ToString()} }
            //                }
            //            }
            //        }
            //    }
            //    }
            //};
            //var response = client.BatchGetItemAsync(request);

            //// Check the response.
            //var result = response.Result;
            //var responses = result.Responses; // The attribute list in the response.

            var batch = _DbContext.ScanAsync<T>(new List<ScanCondition>() {
            new ScanCondition("UserId", ScanOperator.Equal,recordId)

                }
            );
            return batch.GetNextSetAsync().Result;

        }

        //public T Get<K,S>(K recordId,string SortCulmnName,S sortKeyStart, S sortKeyEnd, bool descending = false)
        //{
        //    DynamoDBOperationConfig conf = new DynamoDBOperationConfig
        //    {
        //        ConditionalOperator = ConditionalOperatorValues.And,
        //        BackwardQuery = descending,
        //    };
        //    conf.QueryFilter.Add(new ScanCondition(SortCulmnName, ScanOperator.Between, sortKeyStart, sortKeyEnd));         
        //    return _DbContext.LoadAsync<T>(recordId, SortCulmnName).Result;
        //}

        //public List<T> Get<K, S>(K recordId, Dictionary<string, AttributeValue> startKey = null, int take =0, string SortCulmnName =null,
        //    S sortKeyStart= default(S) , S sortKeyEnd =default(S) , bool descending = false)
        //{
        //    ScanRequest req = new ScanRequest();
        //    if (startKey != null)
        //        req.ExclusiveStartKey = startKey;
        //    if (take != 0)
        //        req.Limit = take;
        //    if()
        //    {
        //        req.ScanFilter.Add(SortCulmnName, Condition.)
        //    }
        //}
    }
}
