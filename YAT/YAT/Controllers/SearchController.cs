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
     [Authorize]
    

    public class SearchController : Controller
    {
        List<String> SortOptions = new List<String> { "Best Match", "Activity Date", "Newest" };

        // GET: Search
         public ActionResult Automatch()
        {

            return RedirectToAction("Index", new { isAutoMatch = true});
        }
        public ActionResult Index(bool isAutoMatch=false)
        {
            Builder b = new Builder();
            User currentUser = b.getCurrentUser(User.Identity.GetUserId());
            string address;
            string InterestedIn;
            bool gender;
            if (currentUser == null)
            {//set default values for Identity-YAT unsynced test user
                address = "94104";
                InterestedIn = "Women";
                gender = true;
            }
            else
            {
                gender = currentUser.Gender;
                address = currentUser.Address;
                InterestedIn =(currentUser.InterestedIn==false)?"Men":"Women";;
            }

            List<User> users = b.queryUsers(minAge: 20, maxAge: 30, gender: gender, InterestedIn: InterestedIn == "Men", address: address, SearcherID: User.Identity.GetUserId(), sortBy: 0);
            ViewBag.minAge =20;
            ViewBag.maxAge = 35;
            ViewBag.address = address;
            ViewBag.sortOptions = SortOptions;
            ViewBag.InterestedIn = InterestedIn;
            ViewBag.Automatch = isAutoMatch;
                //new SelectList(new[] { "Best Match", "Activity Date","Newest" });
            return View(users);
        }



        [HttpPost]
        public ActionResult Index(int minAge, int maxAge, string FindGender, string InterestedIn, string sortVal, string address)
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

            List<User> users = b.queryUsers(minAge: minAge, maxAge: maxAge, gender: FindGender == "Men", InterestedIn: InterestedIn == "Men", address: address, SearcherID: User.Identity.GetUserId(), sortBy: sortBy);
            ViewBag.minAge = minAge;
            ViewBag.maxAge = maxAge;
            ViewBag.address = address;
            ViewBag.sortOptions = SortOptions;
            ViewBag.InterestedIn = InterestedIn;
            ViewBag.FindGender = FindGender;
            ViewBag.Automatch = false;
            return View(users);



        }
    }
}