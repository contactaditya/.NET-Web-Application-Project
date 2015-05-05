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

            Assert.AreEqual(mikeSueConvo.ToList()[0].Text, "Hi there");
            Assert.AreEqual(mikeSueConvo.ToList()[1].Text, "Hello");
            Assert.AreEqual(mikeSueConvo.ToList()[2].Text, "wanna go program some tests??!?!!");
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

            Assert.AreEqual(mikeSueConvo.ToList()[0].Text, "Hi there");
            Assert.AreEqual(mikeSueConvo.ToList()[1].Text, "Hello");
            Assert.AreEqual(mikeSueConvo.ToList()[2].Text, "wanna go program some tests??!?!!");

            var sueMikeConvo = msging.getConversation(sue.Id, mike.Id).ToList();

            Assert.AreEqual(mikeSueConvo.ToList()[0].Text, sueMikeConvo.ToList()[0].Text);
            Assert.AreEqual(mikeSueConvo.ToList()[1].Text, sueMikeConvo.ToList()[1].Text);
            Assert.AreEqual(mikeSueConvo.ToList()[2].Text, sueMikeConvo.ToList()[2].Text); 
            Assert.AreEqual(mikeSueConvo.Count, 3);
            Assert.AreEqual(sueMikeConvo.Count, 3);

        }

    }
}
