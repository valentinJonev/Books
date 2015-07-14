using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Models;

namespace BooksUI.Controllers
{
    public partial class UserController : Controller
    {
        private IUnitOfWork unitOfWork;
        public UserController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public virtual ActionResult Login()
        {
            if (Session["User"] != null)
            {
                return RedirectToAction(MVC.Home.Index());   
            }
            return View();
        }

        [HttpPost]
        public virtual ActionResult Login(string Username, string Password)
        {
            var user = unitOfWork.UserRepository.Get(u => u.Username == Username && u.Password == Password);
            if (user.Count() > 0)
            {
                Session["User"] = user.First();
                return RedirectToAction(MVC.Home.Index());
            }
            ModelState.AddModelError("error", "Invalid username or password!");
            return View();
        }

        [HttpGet]
        public virtual ActionResult Register()
        {
            if (Session["User"] != null)
            {
                return RedirectToAction(MVC.Home.Index());
            }
            return View();
        }

        [HttpPost]
        public virtual ActionResult Register(string Username, string Password, string RepeatPassword, int Age)
        {
            if (Password == RepeatPassword)
            {
                if (unitOfWork.UserRepository.Get(u => u.Username == Username).Count() == 0)
                {
                    Random random = new Random();
                    User user = new User { Id = random.Next(0, 25565), Username = Username, Password = Password, Age = Age };
                    unitOfWork.UserRepository.Insert(user);
                    unitOfWork.Save();
                    return View(MVC.User.Login());
                }
                ModelState.AddModelError("error", "User already registered!");
                return View();
            }
            ModelState.AddModelError("error", "Passwords do not mach!");
            return View();
        }

        public virtual ActionResult Logout()
        {
            if (Session["User"] != null)
            {
                Session.Abandon();
            }
            return RedirectToAction(MVC.User.Login());
        }
    }
}