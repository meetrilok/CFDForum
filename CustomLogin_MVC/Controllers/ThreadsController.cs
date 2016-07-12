using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CustomLogin_MVC.Models;
using Microsoft.AspNet.SignalR;
using CustomLogin_MVC.Hubs;
using System.Web.Security;
using System.Reflection;

namespace CustomLogin_MVC.Controllers
{
    
    public class ThreadsController : Controller
    {
        private CustomLogin_MVCContext db = new CustomLogin_MVCContext();




        // GET: Threads
        public ActionResult Index()
        {
            try
            {
                ViewBag.allmodels = db.UserModels;
                if (Request.IsAuthenticated)
                {
                    UserModel um = db.UserModels.Single(x => x.Email == User.Identity.Name);
                    ViewBag.usermodel = um;
                }
            }
            catch (Exception ex)
            {
                FormsAuthentication.SignOut();
            }

            //SendMessage("ThreadsOpened");
            return View(db.Threads.ToList());
        }
        


        // GET: Threads/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Thread thread = db.Threads.Find(id);
        //    if (thread == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(thread);
        //}
        //[System.Web.Mvc.Authorize]
        // GET: Threads/Create
        public ActionResult Create()
        {
            if (Request.IsAuthenticated)
            {
                UserModel um = db.UserModels.Single(x => x.Email == User.Identity.Name);
                ViewBag.usermodel = um;
            }
            return View();
        }

        // POST: Threads/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ThreadId,User,Threadtitle,ThreadText,ThreadType")] Thread thread)
        {
            if (ModelState.IsValid)
            {
                if (Request.IsAuthenticated)
                {
                    UserModel um = db.UserModels.Single(x => x.Email == User.Identity.Name);
                    thread.User = um.Name;
                }
                else
                {
                    thread.User = thread.User + "(Guest User)";
                }
                thread.solved = false;
                thread.ThreadTime = DateTime.Now.AddHours(5).AddMinutes(30);
                thread.updatedTime= DateTime.Now.AddHours(5).AddMinutes(30);
                db.Threads.Add(thread);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(thread);
        }
        [System.Web.Mvc.Authorize]
        // GET: Threads/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Thread thread = db.Threads.Find(id);
            if (thread == null)
            {
                return HttpNotFound();
            }
            return View(thread);
        }
        [HttpPost]
       
        public ActionResult solved(int?  tid)
        {
            
            Thread t1 = db.Threads.Single(x => x.ThreadId == tid);
            if(t1.solved==true)
            {
                t1.solved = false;
                db.Entry(t1).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                t1.solved = true;
                db.Entry(t1).State = EntityState.Modified;
                db.SaveChanges();
            }
            return View("../replies/Showreplies/", t1.ThreadId);
        }

        // POST: Threads/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [System.Web.Mvc.Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ThreadId,Threadtitle,ThreadText")] Thread thread)
        {
                Thread pt = db.Threads.Find(thread.ThreadId);
                pt.Threadtitle = thread.Threadtitle;
                pt.ThreadText = thread.ThreadText;

                db.Entry(pt).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
          
        }

        // GET: Threads/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Thread thread = db.Threads.Find(id);
            if (thread == null)
            {
                return HttpNotFound();
            }
            return View(thread);
        }

        // POST: Threads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Thread thread = db.Threads.Find(id);
            db.Threads.Remove(thread);
            db.SaveChanges();
            return RedirectToAction("Index");
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
