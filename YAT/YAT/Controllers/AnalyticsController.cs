using BusinessLayer;
using DataLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace YAT.Controllers
{
    public class AnalyticsController : Controller
    {
        private Analytics business = new Analytics();

        // GET: Analytics
        public ActionResult Index()
        {
            ViewBag.Message = "This is the analytics page.";

            ViewData["movieRank"]          = business.movieRank();;
            ViewData["genderCount"]        = business.genderCount();
            ViewData["ageRank"]            = business.ageRank();
            ViewData["registrationMonths"] = business.registrationMonths();
            ViewData["zipCount"]           = business.zipCount();

            using (var dbContext = new YATContext())
            {
                ViewData["user"] = dbContext.User.Where(p => p.FirstName.Contains("Paul")).FirstOrDefault();
            }
            return View();
        }

        public ActionResult FileUpload(HttpPostedFileBase file)
        {
            if (file != null)
            {
                
                using (var dbContext = new YATContext())
                {
                    User paul = dbContext.User.Where(p => p.FirstName.Contains("Paul")).FirstOrDefault();

                    string picName = System.IO.Path.GetFileName(file.FileName);
                    var folder = Directory.CreateDirectory(Server.MapPath("~/Pics/"+paul.Id.ToString()));
                    file.SaveAs(System.IO.Path.Combine(folder.FullName, picName));

                    paul.Photo = picName;
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