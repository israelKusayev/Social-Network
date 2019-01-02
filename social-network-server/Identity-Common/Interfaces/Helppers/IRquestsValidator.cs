using Identity_Common.models;

namespace Identity_Common.Interfaces.Helppers
{
    public interface IRquestsValidator
    {
        string ValidateUser(User user, string tokenId);
    }
}