using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YAT.Controllers
{
    public class UserProfileController : Controller
    { 
        // 
        // GET: UserProfile
 
        public ActionResult UserProfile() 
        { 
            return View(); 
        } 
 
        // 
        // GET: UserProfile/Welcome
 
        public ActionResult Welcome() 
        { 
            return View(); 
        } 
    } 
}

