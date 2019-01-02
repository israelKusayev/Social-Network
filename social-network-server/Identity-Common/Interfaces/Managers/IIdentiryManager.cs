using Identity_Common.models;

namespace Identity_Common.Interfaces.Managers
{
    public interface IIdentiryManager
    {
        bool CreateUser(User user);
        User FindUser(string userId);
        bool UpdateUser(User user);
    }
}