using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using BusinessLayer;
using System.Diagnostics;
namespace YAT.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult Index()
        {
            Builder b = new Builder();

            //Test searching users
            List<User> users = b.queryUsers(minAge: 20, maxAge: 30, gender: true, zipcode: 11791, SearcherID: 1, sortBy: 0);
           Debug.Print ("Count is " + Convert.ToString(users.Count()));
            return View(users);
        }
    }
}