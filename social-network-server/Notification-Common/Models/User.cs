using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Notification_Common.Models
{
    public class User
    {
        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; }
    }
}
