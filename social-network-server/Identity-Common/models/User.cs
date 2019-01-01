using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity_Common.models
{
    [DynamoDBTable("Users")]
    public class User
    {
        [DynamoDBProperty("_id")]
        [DynamoDBHashKey]
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string WorkPlace { get; set; }
        public int Age { get; set; }
    }
}
