namespace Notification_Common.Interfaces.Managers
{
    public interface ITokenManager
    {
        string GetUserId(string token);
    }
}