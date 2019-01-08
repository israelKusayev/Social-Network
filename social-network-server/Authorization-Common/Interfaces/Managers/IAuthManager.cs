using Authorization_Common.Models;
using Authorization_Common.Models.DTO;

namespace Authorization_Common.Interfaces.Managers
{
    public interface IAuthManager
    {
        string AddUserToIdentity(string userId, string email, string token);
        UserAuth Login(LoginDTO model);
        UserFacebook LoginFacebook(FacebookLoginDTO model);
        UserAuth Register(RegisterDTO model);
        bool ResetPassword(ResetPasswordDTO model);

        string RefreshToken(string token);
        bool AddUserToSocial(string userId, string username, string token);
    }
}