using Authorization_Common.Models;
using Authorization_Common.Models.DTO;

namespace Authorization_Common.Interfaces.Managers
{
    public interface IAuthManager
    {
        string AddUserToDb(string userId, string email, string token);
        UserAuth Login(LoginDTO model);
        UserFacebook LoginFacebook(FacebookLoginDTO model);
        UserAuth Register(RegisterDTO model);
        bool ResetPassword(ResetPasswordDTO model);
    }
}