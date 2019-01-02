using Microsoft.VisualStudio.TestTools.UnitTesting;
using Identity_Bl.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity_Bl.Managers.Tests
{
    [TestClass()]
    public class TokenManagerTests
    {
        [TestMethod()]
        public void IsValid_validTokenReturnsTrue()
        {
            string validToken = "";
            TokenManager tokenManager = new TokenManager();
            Assert.IsTrue(tokenManager.IsValid(validToken));
        }

        [TestMethod()]
        public void IsValid_invalidTokenReturnsFalse()
        {
            string invalidToken = "";
            TokenManager tokenManager = new TokenManager();
            Assert.IsFalse(tokenManager.IsValid(invalidToken));
        }
    }
}