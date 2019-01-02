using Authorization_Common.Models.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorization_Bl
{
    public class Validations
    {
        public static string ValidateRegister(RegisterDTO registerDTO)
        {
            if (string.IsNullOrWhiteSpace(registerDTO.Username))
                return "Username is required";
            if (string.IsNullOrWhiteSpace(registerDTO.Password))
                return "Password is required";
            if (string.IsNullOrWhiteSpace(registerDTO.Email))
                return "Email is required";
            if (registerDTO.Password.Length < 8)
                return "password must be at least 8 characters";
            if (!new EmailAddressAttribute().IsValid(registerDTO.Email))
                return "invalid email address";
            return null;
        }
        public static string ValidateLogin(LoginDTO loginDTO)
        {
            if (string.IsNullOrWhiteSpace(loginDTO.Username))
                return "Username is required";
            if (string.IsNullOrWhiteSpace(loginDTO.Password))
                return "Password is required";
            if (loginDTO.Password.Length < 8)
                return "password must be at least 8 characters";
            return null;
        }
    }
}
