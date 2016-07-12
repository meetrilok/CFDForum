using CustomLogin_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CustomLogin_MVC.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LogIn(Models.UserModel userr)
        {
            //if (ModelState.IsValid)
            //{
            if (IsValid(userr.Email, userr.Password))
            {
                FormsAuthentication.SetAuthCookie(userr.Email, false);
                
                return RedirectToAction("Index", "Threads");
            }
            else
            {
                ModelState.AddModelError("", "Login details are wrong.");
            }

            return View(userr);
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Models.UserModel user)
        {
            if (ModelState.IsValid)
            {
                using (var db = new CustomLogin_MVCContext())
                {
                    var crypto = new SimpleCrypto.PBKDF2();
                    var encrypPass = crypto.Compute(user.Password);
                    var newUser = db.UserModels.Create();
                    newUser.Name = user.Name;
                    newUser.Email = user.Email;
                    newUser.Password = encrypPass;
                    newUser.PasswordSalt = crypto.Salt;
                    newUser.Id = Guid.NewGuid();
                    newUser.ReplyCount = 0;
                    db.UserModels.Add(newUser);
                    db.SaveChanges();
                    return RedirectToAction("LogIn", "User");
                }
            }
            else
            {
                ModelState.AddModelError("", "Data is not correct");
            }



            return View();
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Threads");
        }

        private bool IsValid(string email, string password)
        {
            var crypto = new SimpleCrypto.PBKDF2();
            bool IsValid = false;

            using (var db = new CustomLogin_MVCContext())
            {
                var user = db.UserModels.FirstOrDefault(u => u.Email == email);
                if (user != null)
                {
                    if (user.Password == crypto.Compute(password, user.PasswordSalt))
                    {
                        IsValid = true;
                    }
                }
            }
            return IsValid;
        }
    }
}
