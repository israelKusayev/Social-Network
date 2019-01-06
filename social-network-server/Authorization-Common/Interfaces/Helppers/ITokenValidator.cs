namespace Authorization_Common.Interfaces.Helppers
{
    public interface ITokenValidator
    {
        dynamic ValidaleRefreshToken(string token);
        dynamic ValidaleToken(string token);
    }
}