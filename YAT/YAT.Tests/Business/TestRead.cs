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
    public class TestRead
    {
        Builder b = new Builder();
        YATContext db = new YATContext();
        Messaging msging = new Messaging();

        [TestMethod]
        public void testNormalCase()
        {
            var bob = new User
            {
                Address = "11791",
                FirstName = "Bob",
                LastName = "ForApples",
                Age = 28,
                Gender = false,
                Photo = "APPLE.jpg",
                Deleted = false,
                InterestedIn = true,
                RegistrationDate = DateTime.Now,
                LastLoginDate = DateTime.Now,

            };
            var mark = new User
            {
                Address = "11791",
                FirstName = "Mark",
                LastName = "man",
                Age = 28,
                Gender = false,
                Photo = "MARK.jpg",
                Deleted = false,
                InterestedIn = true,
                RegistrationDate = DateTime.Now,
                LastLoginDate = DateTime.Now,

            };
            var msg = new Message
            {
                Text = "spaghetti",
                From = bob,
                To = mark,
                Date = DateTime.Now,
                Read = false
            };
            db.User.Add(mark);
            db.User.Add(bob);
            db.Messages.Add(msg);
            db.SaveChanges();

            msging.read(mark.Id, msg.Id);
            db = new YATContext();
            Message mess = db.Messages.Where(m => m.Id.Equals(msg.Id)).FirstOrDefault();
            Assert.IsTrue(mess.Read);

        }

        [TestMethod]
        public void testSenderRead()
        {
            var bob = new User
            {
                Address = "11791",
                FirstName = "Bob",
                LastName = "ForApples",
                Age = 28,
                Gender = false,
                Photo = "APPLE.jpg",
                Deleted = false,
                InterestedIn = true,
                RegistrationDate = DateTime.Now,
                LastLoginDate = DateTime.Now,

            };
            var mark = new User
            {
                Address = "11791",
                FirstName = "Mark",
                LastName = "man",
                Age = 28,
                Gender = false,
                Photo = "MARK.jpg",
                Deleted = false,
                InterestedIn = true,
                RegistrationDate = DateTime.Now,
                LastLoginDate = DateTime.Now,

            };
            var msg = new Message
            {
                Text = "spaghetti",
                From = bob,
                To = mark,
                Date = DateTime.Now,
                Read = false
            };
            db.User.Add(mark);
            db.User.Add(bob);
            db.Messages.Add(msg);
            db.SaveChanges();

            msging.read(bob.Id, msg.Id);
            db = new YATContext();
            Message mess = db.Messages.Where(m => m.Id.Equals(msg.Id)).FirstOrDefault();
            Assert.IsFalse(mess.Read);
        }

    }
}
