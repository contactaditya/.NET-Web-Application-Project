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
    public class TestGetConversation
    {
        Builder b = new Builder();
        YATContext db = new YATContext();
        Messaging msging = new Messaging();

        [TestMethod]
        public void testNormalConversationCase(){

            User mike = db.User.Where(m => m.FirstName.Contains("Mike")).FirstOrDefault();
            User sue = db.User.Where(s => s.FirstName.Contains("Sue")).FirstOrDefault();

            var mikeSueConvo = msging.getConversation(mike.Id, sue.Id).ToList();

            Assert.AreEqual(mikeSueConvo[0], "Hi there");
            Assert.AreEqual(mikeSueConvo[1], "Hello");
            Assert.AreEqual(mikeSueConvo[2], "wanna go program some tests??!?!!");
            Assert.AreEqual(mikeSueConvo.Count, 3);

        }

        [TestMethod]
        public void testDoesntExistCase()
        {
            User mike = db.User.Where(m => m.FirstName.Contains("Mike")).FirstOrDefault();

            var convo = msging.getConversation(mike.Id, mike.Id).ToList();

            Assert.AreEqual(convo.Count, 0);
            
        }

        [TestMethod]
        public void testReverseIsEqualCase()
        {
            User mike = db.User.Where(m => m.FirstName.Contains("Mike")).FirstOrDefault();
            User sue = db.User.Where(s => s.FirstName.Contains("Sue")).FirstOrDefault();

            var mikeSueConvo = msging.getConversation(mike.Id, sue.Id).ToList();

            Assert.AreEqual(mikeSueConvo[0], "Hi there");
            Assert.AreEqual(mikeSueConvo[1], "Hello");
            Assert.AreEqual(mikeSueConvo[2], "wanna go program some tests??!?!!");

            var sueMikeConvo = msging.getConversation(sue.Id, mike.Id).ToList();

            Assert.AreEqual(mikeSueConvo[0], sueMikeConvo[0]);
            Assert.AreEqual(mikeSueConvo[1], sueMikeConvo[1]);
            Assert.AreEqual(mikeSueConvo[2], sueMikeConvo[2]); 
            Assert.AreEqual(mikeSueConvo.Count, 3);
            Assert.AreEqual(sueMikeConvo.Count, 3);

        }

    }
}
