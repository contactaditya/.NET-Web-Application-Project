using BusinessLayer;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YAT.Controllers
{
    public class AnalyticsController : Controller
    {
        private Analytics business = new Analytics();

        // GET: Analytics
        public ActionResult Index()
        {
            ViewBag.Message = "This is the analytics page.";

            return View(business.movieRank().ToList());
        }
    }
}