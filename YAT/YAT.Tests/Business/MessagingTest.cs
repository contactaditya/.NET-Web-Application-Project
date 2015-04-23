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
    public class MessagingTest
    {
        Builder b = new Builder();
        YATContext db = new YATContext();
        Messaging msging = new Messaging();

        [TestMethod]
        public void ConversationTest(){

            User mike = db.User.Where(m => m.FirstName.Contains("Mike")).FirstOrDefault();
            User sue = db.User.Where(s => s.FirstName.Contains("Sue")).FirstOrDefault();

            var mikeSueConvo = msging.getConversation(mike.Id, sue.Id).ToList();

            Assert.AreEqual(mikeSueConvo[0], "Hi there");
            Assert.AreEqual(mikeSueConvo[1], "Hello");
            Assert.AreEqual(mikeSueConvo[2], "wanna go program some tests??!?!!");

        }

    }
}
