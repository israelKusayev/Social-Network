using Authorization_Common.Interfaces.Repositories;
using Authorization_Common.Models;
using Authorization_Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Lifetime;

namespace Authorization_Bl
{
    public class UnityInjectorBl
    {
        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterSingleton<IDynamoDbRepository<UserAuth>, DynamoDbRepository<UserAuth>>();
            container.RegisterSingleton<IDynamoDbRepository<UserFacebook>, DynamoDbRepository<UserFacebook>>();
            container.RegisterSingleton<IDynamoDbRepository<TokenHistory>, DynamoDbRepository <TokenHistory>> ();
            container.RegisterSingleton<IDynamoDbRepository<BlockedUser>, DynamoDbRepository<BlockedUser>>();
        }
    }
}
