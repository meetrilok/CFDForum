using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CustomLogin_MVC.Models;

namespace CustomLogin_MVC.Controllers
{
    public class RepliesController : Controller
    {
        private CustomLogin_MVCContext db = new CustomLogin_MVCContext();

        // GET: Replies
        public ActionResult Index()
        {
            var replies = db.Replies.Include(r => r.Thread);
            return View(replies.ToList());
        }
        
        public ActionResult ShowReplies(int? threadId)
        {
            
            if (Request.IsAuthenticated)
            {
                UserModel um = db.UserModels.Single(x => x.Email == User.Identity.Name);
                ViewBag.usermodel = um;
               
               if(db.Seens.Where(x => x.ThreadId == (int)threadId).ToList().Count==0)
                {
                    Seen s1 = new Seen
                    {
                        ThreadId = (int)threadId,
                        UserId = um.Id
                    };
                    db.Seens.Add(s1);
                    db.SaveChanges();
                }
                
            }
            var replies = db.Replies.Where(r => r.ThreadId == threadId).ToList();
            ViewBag.replies = replies;
            ViewBag.allModels = db.UserModels;
            ViewBag.currThread = db.Threads.Find(threadId);
            return View();
        }

        [HttpPost]
        //[System.Web.Mvc.Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult ShowReplies([Bind(Include = "ReplyId,ReplyText,ReplyUser,ThreadId")]Reply reply)
        {
           
                if (Request.IsAuthenticated)
                {
                    UserModel um = db.UserModels.Single(x => x.Email == User.Identity.Name);
                    reply.ReplyUser= um.Name;
                reply.AuthReply = true;
                um.ReplyCount++;
                db.Entry(um).State = EntityState.Modified;
                ViewBag.usermodel = um;

                }
                else
            {
                reply.ReplyUser = reply.ReplyUser + "(Guest User)";
                reply.AuthReply = false;
            }
                reply.Answer = false;
                reply.ReplyTime = DateTime.Now.AddHours(5).AddMinutes(30);
                Thread thread = db.Threads.Find(reply.ThreadId);
                thread.updatedTime = reply.ReplyTime;
                db.Entry(thread).State = EntityState.Modified;
                
                db.Replies.Add(reply);
                db.SaveChanges();
                var replies = db.Replies.Where(r => r.ThreadId == reply.ThreadId).ToList();
                ViewBag.replies = replies;
                ViewBag.currThread = db.Threads.Find(reply.ThreadId);
            ViewBag.allModels = db.UserModels;

            return View();
           
        }
        [HttpPost]
        public ActionResult Answered(int rid)
        {
            Reply r1 = db.Replies.Single(x => x.ReplyId== rid);
            if (r1.Answer== true)
            {
                r1.Answer = false;
                db.Entry(r1).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                r1.Answer = true;
                db.Entry(r1).State = EntityState.Modified;
                db.SaveChanges();
            }
            return View("../replies/Showreplies/", r1.ThreadId);
        }

        // GET: Replies/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Reply reply = db.Replies.Find(id);
        //    if (reply == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(reply);
        //}

        // GET: Replies/Create
        //public ActionResult Create()
        //{
        //    ViewBag.ThreadId = new SelectList(db.Threads, "ThreadId", "User");
        //    return View();
        //}

        // POST: Replies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ReplyId,ReplyUser,ReplyText,ReplyTime,ThreadId")] Reply reply)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Replies.Add(reply);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.ThreadId = new SelectList(db.Threads, "ThreadId", "User", reply.ThreadId);
        //    return View(reply);
        //}
        [Authorize]
        // GET: Replies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reply reply = db.Replies.Find(id);
            if (reply == null)
            {
                return HttpNotFound();
            }
            ViewBag.ThreadId = new SelectList(db.Threads, "ThreadId", "User", reply.ThreadId);
            return View(reply);
        }

        // POST: Replies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        
        public ActionResult Edit([Bind(Include = "ReplyId,ReplyText")] Reply reply)
        {
            Reply rp = db.Replies.Find(reply.ReplyId);
            rp.ReplyText = reply.ReplyText;
                db.Entry(rp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ShowReplies", rp.Thread);
            
        }
        [Authorize]
        // GET: Replies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reply reply = db.Replies.Find(id);
            if (reply == null)
            {
                return HttpNotFound();
            }
            return View(reply);
        }
        [Authorize]
        // POST: Replies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reply reply = db.Replies.Find(id);
            int threadId = reply.ThreadId;
            var replies = db.Replies.Where(r => r.ThreadId == reply.ThreadId).ToList();
            ViewBag.replies = replies;
            ViewBag.currThread = db.Threads.Find(reply.ThreadId);
            ViewBag.allModels = db.UserModels;
            UserModel um1 = db.UserModels.Single(x => x.Name == reply.ReplyUser);
            if (um1 != null)
            {
                um1.ReplyCount--;
                db.Entry(um1).State = EntityState.Modified;
            }
            db.Replies.Remove(reply);
            db.SaveChanges();
            Thread t1 = db.Threads.Find(threadId);
            return RedirectToAction("../replies/ShowReplies", t1);
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
