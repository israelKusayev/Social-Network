using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace social_network_server.Models.DTO
{
    public class RegisterDTO
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}