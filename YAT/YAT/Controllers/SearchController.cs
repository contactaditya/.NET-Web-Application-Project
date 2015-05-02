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
        public ActionResult Index()
        {
            Builder b = new Builder();
            User currentUser = b.getCurrentUser(User.Identity.GetUserId());
            string address;
            string InterestedIn;
            if (currentUser == null)
            {//set default values for Identity-YAT unsynced test user
                address = "94104";
                InterestedIn = "Women";
            }
            else
            {
                address = currentUser.Address;
                InterestedIn =(currentUser.InterestedIn==false)?"Men":"Women";;
            }
            
            List<User> users = b.queryUsers(minAge: 20, maxAge: 30, gender: true, address: address, SearcherID: User.Identity.GetUserId(), sortBy: 0);
            ViewBag.minAge =20;
            ViewBag.maxAge = 35;
            ViewBag.address = address;
            ViewBag.sortOptions = SortOptions;
            ViewBag.LookingFor = InterestedIn;
            ViewBag.Type = "Search";
                //new SelectList(new[] { "Best Match", "Activity Date","Newest" });
            return View(users);
        }



        [HttpPost]
        public ActionResult Index(int minAge,int maxAge, string FindGender,string   LookForGender, string sortVal, string address)
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
           
            List<User> users = b.queryUsers(minAge: minAge, maxAge: maxAge, gender: FindGender ==  "Women", address:address  , SearcherID: User.Identity.GetUserId(), sortBy: sortBy);
            ViewBag.minAge = minAge;
            ViewBag.maxAge = maxAge;
            ViewBag.address = address;
            ViewBag.sortOptions = SortOptions;
            ViewBag.LookForGender = LookForGender;
            ViewBag.FindGender = FindGender;
            ViewBag.Type = "Search";
            return View(users);



        }
    }
}