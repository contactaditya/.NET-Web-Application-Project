using DataLayer;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YAT.Models;
using BusinessLayer;
namespace YAT.Controllers
{
    public class UserProfileController : Controller
    { 
        public ActionResult UserProfile(string userID = "")
        {
            Builder b = new Builder();

         //   var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
           // var currentUser = manager.FindById(User.Identity.GetUserId());
            if (userID=="") userID=User.Identity.GetUserId();
            User YATUser = b.getCurrentUser(userID);
            //using (var dbContext = new YATContext())
            //{
            //    YATUser = dbContext.User.Where(p => p.Id.Contains(currentUser.Id)).FirstOrDefault();
            //}

            return View(YATUser); 
        } 
 
        // 
        // GET: UserProfile/Welcome
 
        public ActionResult Welcome() 
        {
            UserProfileController user = new UserProfileController();
            return View(); 
        } 
    } 
}

