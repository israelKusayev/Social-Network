using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authorization_Common.Models;
using Authorization_Common.Models.DTO;
using Authorization_Dal;

namespace Authorization_Bl.Managers
{
    public class AuthManager
    {
        DynamoDbRepository<UserAuth> _authRepository;
        DynamoDbRepository<UserFacebook> _oAuthRepository;
        public AuthManager()
        {
            _authRepository = new DynamoDbRepository<UserAuth>();
            _oAuthRepository = new DynamoDbRepository<UserFacebook>();
        }

        public UserAuth Register(RegisterDTO model)
        {
            var guid = Guid.NewGuid().ToString();
            return _authRepository.Add(new UserAuth() { Username = model.Username, Password = model.Password, UserId = guid });
        }

        public UserAuth Login(LoginDTO model)
        {
            var user = _authRepository.Get(model.Username);
            if (user == null && user.Password != model.Password)
            {
                return null;
            }
            return user;
        }

        public UserFacebook LoginFacebook(FacebookLoginDTO model)
        {
            var existingUser = _oAuthRepository.Get(model.FacebookId);
            if (existingUser != null)
            {
                return existingUser;
            }
            else
            {
                var guid = Guid.NewGuid().ToString();
                return _oAuthRepository.Add(new UserFacebook() { FacebookId = model.FacebookId, Username = model.Username, UserId = guid });
            }
        }
    }
}
