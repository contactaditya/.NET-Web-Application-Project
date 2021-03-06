﻿using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using DataLayer;
using YAT.Models;

namespace YAT.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Yet Another Tinder";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact Webmasters";

            return View();
        }

        public ActionResult UserProfile()
        {
            return View();
        }
    }
}