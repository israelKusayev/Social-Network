using Authorization_Common.Interfaces.Repositories;
using Authorization_Common.Models;
using Authorization_Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace Authorization_Bl
{
    public class UnityInjectorBl
    {
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your type's mappings here.
            // container.RegisterType<IProductRepository, ProductRepository>();

            container.RegisterSingleton<IDynamoDbRepository<UserAuth>, DynamoDbRepository<UserAuth>>();
            container.RegisterSingleton<IDynamoDbRepository<UserFacebook>, DynamoDbRepository<UserFacebook>>();
            container.RegisterSingleton<IDynamoDbRepository<TokenHistory>, DynamoDbRepository <TokenHistory>> ();
            container.RegisterSingleton<IDynamoDbRepository<BlockedUser>, DynamoDbRepository<BlockedUser>>();
        }
    }
}
