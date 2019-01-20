using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Notification_Common.Models
{

    [DynamoDBTable("NotifiactionKeys")]
    public class NotificationKey
    {
        [DynamoDBHashKey]
        public string UserId { get; set; }
        public string ConnectionId { get; set; }
    }
}
