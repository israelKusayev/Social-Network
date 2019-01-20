using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Notification_Common.Models
{
    [DynamoDBTable("Notifications")]
    public class Notification
    {
        [DynamoDBHashKey]
        public string UserId { get; set; }

        [DynamoDBRangeKey]
        public long TimeStamp { get; set; }
        public string DataJson {get; set;}
        public string Methood { get; set; }
    }
}
