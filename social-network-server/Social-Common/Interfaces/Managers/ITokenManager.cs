namespace Social_Common.Interfaces.Managers
{
    public interface ITokenManager
    {
        string GetUserId(string token);
        bool IsValid(string token);
    }
}