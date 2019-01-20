using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notification_Fe.Provider
{
    public class UsersProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            return "34b4940f-9dfb-481e-a011-f16cb8ee8dd7";
            //return connection.User.Claims;
        }
    }
}
