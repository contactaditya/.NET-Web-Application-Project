using BusinessLayer;
using DataLayer;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using YAT.Models;

namespace YAT.Controllers
{
    //[Authorize]
    public class AnalyticsController : Controller
    {
        private Analytics business = new Analytics();
        private Users users = new Users();

        public ActionResult Index()
        {
            ViewBag.Message = "This is the analytics page.";

            ViewData["movieRank"]          = business.movieRank();;
            ViewData["genderCount"]        = business.genderCount();
            ViewData["ageRank"]            = business.ageRank();
            ViewData["registrationMonths"] = business.registrationMonths();
            ViewData["zipCount"]           = business.zipCount();

            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var currentUser = manager.FindById(User.Identity.GetUserId());
            User YATUser;
            using (var dbContext = new YATContext())
            {
                YATUser = dbContext.User.Where(p => p.Id.Contains(currentUser.Id)).FirstOrDefault();
            }

            return View(YATUser);
        }

        public ActionResult FileUpload(HttpPostedFileBase file)
        {
            if (file != null)
            {
                using (var dbContext = new YATContext())
                {
                    var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                    var currentUser = manager.FindById(User.Identity.GetUserId());
                    var YATUser = dbContext.User.Where(p => p.Id.Equals(currentUser.Id)).FirstOrDefault();

                    string picName = System.IO.Path.GetFileName(file.FileName);
                    var folder = Directory.CreateDirectory(Server.MapPath("~/Pics/" + YATUser.Id));
                    file.SaveAs(System.IO.Path.Combine(folder.FullName, picName));

                    YATUser.Photo = picName;
                    dbContext.SaveChanges();
                }

                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                }

            }
            return RedirectToAction("Index", "Analytics");
        }
    }
}