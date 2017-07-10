using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThreeTier.Service;
using ThreeTier.Service.DemoServices;
using ThreeTier.Data;

namespace ThreeTier.Tests.Service
{
    /// <summary>
    /// Summary description for DemoUserServiceTest
    /// </summary>
    [TestClass]
    public class DemoUserServiceTest
    {
        UserBLInterface srv;

        [TestMethod]
        public void DemoUserService_Valid_Users_Can_Login()
        {
            srv = new UserBL();
            Assert.IsTrue(srv.Login("Robert","Robert"));
        }

        [TestMethod]
        public void DemoUserService_Invalid_Users_Can_Not_Login()
        {
            srv = new UserBL();
            Assert.IsFalse(srv.Login("Robert", "Robert1"));
        }

        [TestMethod]
        public void DemoUserService_Should_Return_Friends()
        {
            srv = new UserBL();
            List<User> users = srv.GetFriendsFromUser("Robert").ToList();
            Assert.IsNotNull(users);
            Assert.AreNotEqual(0, users.Count);
            Assert.AreEqual("Oli", users[0].Login);
        }
    }
}
