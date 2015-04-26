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
    public class TestSendMessage
    {
        Builder b = new Builder();
        YATContext db = new YATContext();
        Messaging msging = new Messaging();

        //not sure what else to test besides this?
        [TestMethod]
        public void testNormalCase()
        {
            User mike = db.User.Where(m => m.FirstName.Contains("Mike")).FirstOrDefault();
            User brad = db.User.Where(s => s.FirstName.Contains("Brad")).FirstOrDefault();

            msging.sendMessage(mike.Id, brad.Id, "woooop");

            Message msg = db.Messages.Where(m => m.Text.Contains("woooop")).FirstOrDefault();

            Assert.IsNotNull(msg);
            Assert.AreEqual(msg.To.Id, mike.Id);
            Assert.AreEqual(msg.From.Id, brad.Id);
        }

    }
}
