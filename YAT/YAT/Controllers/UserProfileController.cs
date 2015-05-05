using DataLayer;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YAT.Models;
using BusinessLayer;
using System.Data.Entity;
using System.IO;
using System.Net;
namespace YAT.Controllers
{
    public class UserProfileController : Controller
    {
        private YATContext db = new YATContext();
        public ActionResult UserProfile(string userID = "")
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
           
            string getID = userID == "" ? User.Identity.GetUserId() : userID;
            string currentUser = User.Identity.GetUserId();
            //var currentUser = manager.FindById(getID);
            var YATUser = new User();
            Connections conn = new Connections();
            using (var dbContext = new YATContext())
            {
               YATUser = dbContext.User.Where(p => p.Id.Contains(getID)).FirstOrDefault();
               conn=  dbContext.Connections.Where(o => o.FromId == currentUser && o.ToId == getID).FirstOrDefault();
            }

            //Disable hide and blocked buttons if already hidden or blocked
            


            if (conn !=null)
            {
                ViewBag.Blocked = conn.IsBlocked;
                ViewBag.Hidden = conn.IsRemoved;
            }
            else
            {
                ViewBag.Blocked = false;
                ViewBag.Hidden = false;
            }
            return View(YATUser); 
        }

        // GET: UserSettings
        public ActionResult UserSettings(string userID = "")
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var currentUser = manager.FindById(User.Identity.GetUserId());
            var YATUser = new User();

            using (var dbContext = new YATContext())
            {
                YATUser = dbContext.User.Where(p => p.Id.Contains(currentUser.Id)).FirstOrDefault();
            }

            return View(YATUser); 
        }

        public ActionResult FileUpload(HttpPostedFileBase file)
        {
            if (file != null)
            {
                using (var dbContext = new YATContext())
                {
                    var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                    var currentUser = manager.FindById(User.Identity.GetUserId());
                    var YATUser = dbContext.User.Where(p => p.Id.Equals(currentUser.Id)).FirstOrDefault();

                    string picName = System.IO.Path.GetFileName(file.FileName);
                    String photo = YATUser.Id + Path.GetExtension(picName);
                    file.SaveAs(Server.MapPath("~/Pics/" + photo));

                    YATUser.Photo = photo;
                    dbContext.SaveChanges();
                }

                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                }

            }
            return RedirectToAction("UserSettings", "UserProfile");
        }

        // GET: /UserSettings/Edit
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }


        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Photo, FirstName,LastName,Age,Gender,InterestedIn,Address,RegistrationDate,LastLoginDate")] User user)
        {
            if (ModelState.IsValid)
            { 
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("UserSettings");
            }
            return View(user);
        }

        [HttpPost]
        public ActionResult SendMessage(string text, string hidden)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var currentUser = manager.FindById(User.Identity.GetUserId());
            User fromUser;
            using (var dbContext = new YATContext())
            {
                fromUser = dbContext.User.Where(p => p.Id.Contains(currentUser.Id)).FirstOrDefault();
            }
            var user = db.User.Find(hidden);
            Messaging msging = new Messaging();
            msging.sendMessage(hidden, fromUser.Id, text);
            return Redirect(Url.Content("~/Messages"));
        }
        [HttpPost]
        public ActionResult UpdateConnection(string buttonName, string ToUser)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
          //  var currentUser = manager.FindById(User.Identity.GetUserId());
            var currentUser = User.Identity.GetUserId();
          //  User fromUser;

            //using (var dbContext = new YATContext())
            //{
            //    fromUser = dbContext.User.Where(p => p.Id.Contains(currentUser.Id)).FirstOrDefault();
            //}
          //  var user = db.User.Find(ToUser);
            //check if connection entry exists for the 2 users
               Connections conn = new Connections();
          //  if (db.Connections.Any(o => o.FromId == currentUser && o.ToId==ToUser ))
           conn=  db.Connections.Where(o => o.FromId == currentUser && o.ToId == ToUser).FirstOrDefault();
            if (conn!=null)
            {
                if (buttonName == "Block" )
                    conn.IsBlocked = true;
                else
                    conn.IsRemoved=true;   
            }
            else //create new entry
            {
              Connections conn2 = new Connections()
                {
                    ToId = ToUser,
                    FromId = currentUser,
                    IsBlocked = (buttonName == "Block") ? true : false,
                    IsRemoved = (buttonName == "Hide") ? true : false
                };
                db.Connections.Add(conn2);
            }
                db.SaveChanges();

            return RedirectToAction("UserProfile", new { userID = ToUser });
        }
    } 

 }


