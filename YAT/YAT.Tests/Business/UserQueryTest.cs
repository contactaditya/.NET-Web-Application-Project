using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using YAT;
using YAT.Controllers;
using BusinessLayer;
using DataLayer;
namespace YAT.Tests.Business
{
    [TestClass]
    public class UserQueryTest
    {
        Builder b = new Builder();
  
        [TestMethod]
        public void qrySortByMatch ()    
        {
            List<User> users = qryTester(UserSort.Match);
            // Assert
          Assert.IsNotNull(users);
        }

        [TestMethod]
        public void qrySortByJoinDate()
        {
            List<User> users = qryTester(UserSort.LastJoin);
            Assert.IsNotNull(users);
        }

        [TestMethod]
        public   void qrySortByLogDate()
        {
            List<User> users = qryTester(UserSort.LastLog);
            Assert.IsNotNull(users);
        }


        public List<User> qryTester(UserSort sortby)
        {

            Builder b = new Builder();

            //Test searching users
            List<User> users = b.queryUsers(minAge: 20, maxAge: 50, gender: false, InterestedIn: true, address:"", SearcherID: "1", sortBy: 0);
            return users;

        }

    }
}
