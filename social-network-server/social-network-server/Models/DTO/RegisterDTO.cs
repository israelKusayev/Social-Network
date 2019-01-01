using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace social_network_server.Models.DTO
{
    public class RegisterDTO
    {
        public string username { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }
}