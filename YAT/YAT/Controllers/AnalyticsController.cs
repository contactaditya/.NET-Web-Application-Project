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

        [OutputCache(Duration=20)]
        public ActionResult Index()
        {
            ViewBag.Message = "This is the analytics page.";

            ViewData["movieRank"]          = business.movieRank();;
            ViewData["genderCount"]        = business.genderCount();
            ViewData["ageRank"]            = business.ageRank();
            ViewData["registrationMonths"] = business.registrationMonths();
            ViewData["stateCount"]         = business.stateCount();
            
            return View();
        }
    }
}