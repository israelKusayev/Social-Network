using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorization_Common.Models
{
    [DynamoDBTable("OAuth")]
    public class UserFacebook
    {
        [DynamoDBHashKey]
        public string FacebookId { get; set; }

        public string Username { get; set; }
        public string UserId { get; set; }
    }
}
