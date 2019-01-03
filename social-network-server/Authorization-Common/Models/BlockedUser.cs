using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorization_Common.Models
{
    [DynamoDBTable("BlockedUsers")]
    public class BlockedUser
    {
        [DynamoDBHashKey]
        public string UserId { get; set; }
    }
}
