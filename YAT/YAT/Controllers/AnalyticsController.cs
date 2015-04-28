using BusinessLayer;
using DataLayer;
using System;
using System.Collections.Generic;
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

            ViewData["movieRank"] = business.movieRank();;
            ViewData["genderCount"] = business.genderCount();
            ViewData["ageRank"] = business.ageRank();
            ViewData["registrationMonths"] = business.registrationMonths();
            ViewData["zipCount"] = business.zipCount();

            return View();
        }
    }
}