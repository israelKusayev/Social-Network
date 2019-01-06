namespace Authorization_Common.Interfaces.Managers
{ 
    public interface IBlockedUsersManager
    {
        bool BlockUser(string userId);
        bool UnBlockUser(string userId);
    }
}