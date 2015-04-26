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
    public class TestGetInbox
    {
        Builder b = new Builder();
        YATContext db = new YATContext();
        Messaging msging = new Messaging();

        //What else to test?
        [TestMethod]
        public void testNormalCase()
        {
            User mike = db.User.Where(m => m.FirstName.Contains("Mike")).FirstOrDefault();

            var inbox = msging.getInbox(mike.Id);

            foreach (Message m in inbox){
                Assert.AreEqual(m.ToId, mike.Id);
            }
        }
    }
}
