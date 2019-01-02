using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorization_Common.Models
{
    [DynamoDBTable("Auth")]
    public class UserAuth
    {
        [DynamoDBHashKey]
        public string Username { get; set; }

        public string Password { get; set; }
        public string UserId { get; set; }
    }
}
