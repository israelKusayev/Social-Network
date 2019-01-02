namespace Identity_Common.interfaces
{
    public interface ITokenManager
    {
        string GetUserId(string token);
        bool IsValid(string token);
    }
}