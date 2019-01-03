using Microsoft.VisualStudio.TestTools.UnitTesting;
using Authorization_Bl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authorization_Common.Models.DTO;

namespace Authorization_Bl.Tests
{
    [TestClass()]
    public class ValidationsTests
    {
        [TestMethod()]
        public void ValidateRegister_seccssededWithValidData()
        {
            RegisterDTO register = new RegisterDTO()
            {
                Username = "un",
                Email = "email@gmail.com",
                Password = "longpass"
            };
            Assert.IsNull(Validations.ValidateRegister(register));
        }

        [TestMethod()]
        public void ValidateRegister_FailWithNullUserName()
        {
            RegisterDTO register = new RegisterDTO()
            {
                Username = null,
                Email = "email@gmail.com",
                Password = "longpass"
            };
            Assert.IsNotNull(Validations.ValidateRegister(register));
        }

        [TestMethod()]
        public void ValidateRegister_FailWithEmptyUserName()
        {
            RegisterDTO register = new RegisterDTO()
            {
                Username = "",
                Email = "email@gmail.com",
                Password = "longpass"
            };
            Assert.IsNotNull(Validations.ValidateRegister(register));
        }

        [TestMethod()]
        public void ValidateRegister_FailWithNullEmail()
        {
            RegisterDTO register = new RegisterDTO()
            {
                Username = "un",
                Email = null,
                Password = "longpass"
            };
            Assert.IsNotNull(Validations.ValidateRegister(register));
        }

        [TestMethod()]
        public void ValidateRegister_FailWithEmptyEmail()
        {
            RegisterDTO register = new RegisterDTO()
            {
                Username = "un",
                Email = "",
                Password = "longpass"
            };
            Assert.IsNotNull(Validations.ValidateRegister(register));
        }

        [TestMethod()]
        public void ValidateRegister_FailWithNullPassword()
        {
            RegisterDTO register = new RegisterDTO()
            {
                Username = "un",
                Email = "email@gmail.com",
                Password = null
            };
            Assert.IsNotNull(Validations.ValidateRegister(register));
        }

        [TestMethod()]
        public void ValidateRegister_FailWithEmptyPassword()
        {
            RegisterDTO register = new RegisterDTO()
            {
                Username = "un",
                Email = "email@gmail.com",
                Password = ""
            };
            Assert.IsNotNull(Validations.ValidateRegister(register));
        }

        [TestMethod()]
        public void ValidateRegister_FailWithShortPassword()
        {
            RegisterDTO register = new RegisterDTO()
            {
                Username = "un",
                Email = "email@gmail.com",
                Password = "123"
            };
            Assert.IsNotNull(Validations.ValidateRegister(register));
        }


        [TestMethod()]
        public void ValidateRegister_FailWithInvalidEmail()
        {
            RegisterDTO register = new RegisterDTO()
            {
                Username = "un",
                Email = "emailgmail.com",
                Password = "longpass"
            };
            Assert.IsNotNull(Validations.ValidateRegister(register));
            register.Email = "email@gmailcom";
            Assert.IsNotNull(Validations.ValidateRegister(register));

        }


        [TestMethod()]
        public void ValidateLogin_seccssededWithValidData()
        {
            LoginDTO login = new LoginDTO()
            {
                Username = "un",
                Password = "longpass"
            };
            Assert.IsNull(Validations.ValidateLogin(login));
        }

        [TestMethod()]
        public void ValidateLogin_FailWithNullUserName()
        {
            LoginDTO login = new LoginDTO()
            {
                Username = null,
                Password = "longPass"
            };
            Assert.IsNotNull(Validations.ValidateLogin(login));
        }

        [TestMethod()]
        public void ValidateLogin_FailWithEmptyUserName()
        {
            LoginDTO login = new LoginDTO()
            {
                Username = "",
                Password = "longpass"
            };
            Assert.IsNotNull(Validations.ValidateLogin(login));
        }

        [TestMethod()]
        public void ValidateLogin_FailWithNullPassword()
        {
            LoginDTO login = new LoginDTO()
            {
                Username = "un",
                Password = null
            };
            Assert.IsNotNull(Validations.ValidateLogin(login));
        }

        [TestMethod()]
        public void ValidateLogin_FailWithEmptyPassword()
        {
            LoginDTO login = new LoginDTO()
            {
                Username = "un",
                Password = ""
            };
            Assert.IsNotNull(Validations.ValidateLogin(login));
        }

        [TestMethod()]
        public void ValidateLogin_FailWithShortPassword()
        {
            LoginDTO login = new LoginDTO()
            {
                Username = "un",
                Password = "pass"
            };
            Assert.IsNotNull(Validations.ValidateLogin(login));
        }

        [TestMethod()]
        public void ValidateResetPassword_seccssededWithValidData()
        {
            ResetPasswordDTO resetPassword = new ResetPasswordDTO()
            {
                Username = "un",
                NewPassword = "longpass"
            };
            Assert.IsNull(Validations.ValidateResetPassword(resetPassword));
        }

        [TestMethod()]
        public void ValidateResetPassword_FailWithNullUserName()
        {
            ResetPasswordDTO resetPassword = new ResetPasswordDTO()
            {
                Username = null,
                NewPassword = "longpass"
            };
            Assert.IsNotNull(Validations.ValidateResetPassword(resetPassword));
        }

        [TestMethod()]
        public void ValidateResetPassword_FailWithEmptyUserName()
        {
            ResetPasswordDTO resetPassword = new ResetPasswordDTO()
            {
                Username = "",
                NewPassword = "longpass"
            };
            Assert.IsNotNull(Validations.ValidateResetPassword(resetPassword));
        }

        [TestMethod()]
        public void ValidateResetPassword_FailWithNullPassword()
        {
            ResetPasswordDTO resetPassword = new ResetPasswordDTO()
            {
                Username = "un",
                NewPassword = null
            };
            Assert.IsNotNull(Validations.ValidateResetPassword(resetPassword));
        }

        [TestMethod()]
        public void ValidateResetPassword_FailWithEmptyPassword()
        {
            ResetPasswordDTO resetPassword = new ResetPasswordDTO()
            {
                Username = "un",
                NewPassword = ""
            };
            Assert.IsNotNull(Validations.ValidateResetPassword(resetPassword));
        }

        [TestMethod()]
        public void ValidateResetPassword_FailWithShortPassword()
        {
            ResetPasswordDTO resetPassword = new ResetPasswordDTO()
            {
                Username = "un",
                NewPassword = "pass"
            };
            Assert.IsNotNull(Validations.ValidateResetPassword(resetPassword));
        }
    }
}