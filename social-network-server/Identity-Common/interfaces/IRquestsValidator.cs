using Identity_Common.models;

namespace Identity_Common.interfaces
{
    public interface IRquestsValidator
    {
        string ValidateUser(User user, string tokenId);
    }
}