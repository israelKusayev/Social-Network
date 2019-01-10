using Social_Common.Interfaces.Repositories;
using SocialDal.Repositories.Neo4j;
using Unity;

namespace SocialBl
{
    public class SocialBlInjector
    {
        public static void Register(IUnityContainer container)
        {
            container.RegisterSingleton<IUsersRepository, Neo4jUsersRepository>();
            container.RegisterSingleton<IPostsRepository, Neo4jPostsRepository>();
            container.RegisterSingleton<ILikesRepository, Neo4jLikesRepository>();
            container.RegisterSingleton<ICommentsRepository, Neo4jCommentsRepository>();
        }
    }
}
