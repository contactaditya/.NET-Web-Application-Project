using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using BusinessLayer;
using System.Diagnostics;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;


namespace YAT.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult Index()
        {
            Builder b = new Builder();
            User currentUser = b.getCurrentUser(User.Identity.GetUserId());
            int zipcode;
            if (currentUser == null)
                zipcode = 11791; 
            else
                zipcode = currentUser.Zip;

        
            //Test searching users
            List<User> users = b.queryUsers(minAge: 20, maxAge: 30, gender: true, zipcode: zipcode, SearcherID: User.Identity.GetUserId(), sortBy: 0);
            ViewBag.minAge =20;
            ViewBag.maxAge = 35;
            ViewBag.ZipCode = zipcode;
            return View(users);
        }

        [HttpPost]
        public ActionResult Index(int minAge,int maxAge, string FindGender,string   LookForGender, string sortVal, int zipcode )
        {
            Builder b = new Builder();
            UserSort sortBy;
            switch (sortVal)
            {
            case "Newest": sortBy=UserSort.LastJoin;
                 break;
            case "Best Match": sortBy=UserSort.Match;
                 break;
                case "Activity Date": sortBy=UserSort.LastLog;
                break;
            default: sortBy=UserSort.Match;
                 break;  
            }
            //Test searching users
            List<User> users = b.queryUsers(minAge: minAge, maxAge: maxAge, gender: FindGender ==  "Women", zipcode:zipcode  , SearcherID: User.Identity.GetUserId(), sortBy: sortBy);
            ViewBag.minAge = minAge;
            ViewBag.maxAge = maxAge;
            ViewBag.ZipCode = zipcode;
            return View(users);
        }
    }
}