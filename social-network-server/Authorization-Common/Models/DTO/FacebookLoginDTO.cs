using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorization_Common.Models.DTO
{
    public class FacebookLoginDTO
    {
        public string FacebookId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }
}
