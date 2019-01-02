using Identity_Common.interfaces;
using Identity_Common.models;
using Idetity_dal;
using Unity;


namespace Identity_Bl
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

            container.RegisterType<IDynamoDbRepository<User>, DynamoDbRepository<User>>();
        }
    }
}
