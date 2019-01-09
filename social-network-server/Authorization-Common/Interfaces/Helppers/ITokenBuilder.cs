namespace Authorization_Common.Interfaces
{
    public interface ITokenBuilder
    {
        string GenerateKey(string userId, string username, bool isAdmin = false,string facebookToken= null);
    }
}