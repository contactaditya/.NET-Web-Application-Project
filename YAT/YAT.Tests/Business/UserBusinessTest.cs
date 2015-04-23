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
    public class UserBusinessTest
    {
        Builder b = new Builder();
        Users users = new Users();
        YATContext db = new YATContext();

        [TestMethod]
        public void getUserLikes()
        {
            b.putData();
            User paul = db.User.Where(p => p.FirstName.Contains("Paul")).FirstOrDefault();
            Likes nemo = db.Likes.Where(l => l.Movie.Contains("Finding Nemo")).FirstOrDefault();
            List<Likes> list = (List<Likes>)users.getUserLikes(paul.Id);
            
            
            // Assert
            Assert.IsTrue(list.Select(a => a.Id).Contains(nemo.Id));
        }

    }
}
