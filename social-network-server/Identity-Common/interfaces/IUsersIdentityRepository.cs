using Identity_Common.models;

namespace Identity_Common.interfaces
{
    public interface IUsersIdentityRepository
    {
        bool Add(User user);
        User Get(string userId);
        bool Update(User user);
    }
}