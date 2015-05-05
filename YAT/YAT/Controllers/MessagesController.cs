using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using BusinessLayer;
using YAT.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace YAT.Controllers
{
    [Authorize]
    public class MessagesController : Controller
    {
        private YATContext db = new YATContext();

        // GET: Messages
        public ActionResult Index()
        {
            
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var currentUser = manager.FindById(User.Identity.GetUserId());
            User user;
            using (var dbContext = new YATContext())
            {
                user = dbContext.User.Where(p => p.Id.Contains(currentUser.Id)).FirstOrDefault();
            }
            string userid = user.Id;
            if (userid == null)
            {
                var messages = db.Messages.Include(m => m.From).Include(m => m.To);
                return View(messages.ToList());
            }
            else
            {
                Messaging msg = new Messaging();
                var messages = msg.getInbox(userid).ToList();
                messages.Reverse();
                List<String> noDupes = new List<String>();
                List<Message> result = new List<Message>();
                foreach (Message message in messages){

                    if (noDupes.Contains(message.From.Id))
                    {
                        continue;
                    }
                    if (userid.Equals(message.From.Id))
                    {
                        if (!noDupes.Contains(message.To.Id))
                        {
                        noDupes.Add(message.To.Id);
                        result.Add(message);
                        }
                        continue;
                    }
                    noDupes.Add(message.From.Id);
                    result.Add(message);
                }

                return View(result.ToList());
            }
        }


        public ActionResult Read(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            Messaging msging = new Messaging();
            var messages = msging.getConversation(message.To.Id, message.From.Id).ToList();
            foreach (Message mess in messages)
            {
                msging.read(mess.To.Id, mess.Id);
            }
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(messages);
        }


        // GET: Messages/Create
        public ActionResult Send()
        {
            ViewBag.FromId = new SelectList(db.User, "Id", "FirstName");
            ViewBag.ToId = new SelectList(db.User, "Id", "FirstName");
            return View();
        }

        // POST: Messages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Text,FromId,ToId,Date,Read")] Message message)
        {
            if (ModelState.IsValid)
            {
                db.Messages.Add(message);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FromId = new SelectList(db.User, "Id", "FirstName", message.FromId);
            ViewBag.ToId = new SelectList(db.User, "Id", "FirstName", message.ToId);
            return View(message);
        }

        // GET: Messages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            ViewBag.FromId = new SelectList(db.User, "Id", "FirstName", message.FromId);
            ViewBag.ToId = new SelectList(db.User, "Id", "FirstName", message.ToId);
            return View(message);
        }

        // POST: Messages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Text,FromId,ToId,Date,Read")] Message message)
        {
            if (ModelState.IsValid)
            {
                db.Entry(message).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FromId = new SelectList(db.User, "Id", "FirstName", message.FromId);
            ViewBag.ToId = new SelectList(db.User, "Id", "FirstName", message.ToId);
            return View(message);
        }

        // GET: Messages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // POST: Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Message message = db.Messages.Find(id);
            db.Messages.Remove(message);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Read(string text, int hidden)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var currentUser = manager.FindById(User.Identity.GetUserId());
            User user;
            using (var dbContext = new YATContext())
            {
                user = dbContext.User.Where(p => p.Id.Contains(currentUser.Id)).FirstOrDefault();
            }
            string userid = user.Id;

            Message message = db.Messages.Find(hidden);
            Messaging msging = new Messaging();
            var toID = message.ToId;
            if (toID.Equals(userid))
            {
                toID = message.FromId;
            }
            msging.sendMessage(toID, userid, text);
            return Redirect(HttpContext.Request.UrlReferrer.AbsoluteUri);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
