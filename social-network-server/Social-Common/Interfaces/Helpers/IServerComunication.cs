using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Common.Interfaces.Helpers
{
    public interface IServerComunication
    {
        void NotifyUser(string action, object obj);
    }
}
