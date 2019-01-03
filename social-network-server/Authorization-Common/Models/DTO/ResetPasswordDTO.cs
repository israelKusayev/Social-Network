using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorization_Common.Models.DTO
{
    public class ResetPasswordDTO
    {
        public string Username { get; set; }
        public string NewPassword { get; set; }
    }
}
