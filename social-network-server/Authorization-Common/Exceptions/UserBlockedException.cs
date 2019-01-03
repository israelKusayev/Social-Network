using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorization_Common.Exceptions
{
    public class UserBlockedException : Exception
    {
        public UserBlockedException():base("User Is Blocked")
        {
        }
    }
}
