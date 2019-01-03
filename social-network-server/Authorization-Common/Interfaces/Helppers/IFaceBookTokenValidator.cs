using Authorization_Common.Models.DTO;

namespace Authorization_Common.Interfaces.Helppers
{
    public interface IFaceBookTokenValidator
    {
        FacebookLoginDTO ValidateAndGet(string facebookToken);
    }
}