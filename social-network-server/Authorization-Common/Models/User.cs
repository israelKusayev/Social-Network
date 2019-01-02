using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorization_Common.Models
{
    public class User
    {
        public string UserId { get; set; }
        public string Email { get; set; }
    }
}
