using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Models;

namespace BooksUI.Controllers
{
    public class UserController : Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork();

        [HttpGet]
        public ActionResult Login()
        {
            if (Session["User"] != null)
            {
                return RedirectToAction("Index", "Home");   
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(string Username, string Password)
        {
            var user = unitOfWork.UserRepository.Get(u => u.Username == Username && u.Password == Password);
            if (user.Count() > 0)
            {
                Session["User"] = user.First();
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("error", "Invalid username or password!");
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            if (Session["User"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Register(string Username, string Password, string RepeatPassword, int Age)
        {
            if (Password == RepeatPassword)
            {
                if (unitOfWork.UserRepository.Get(u => u.Username == Username) == null)
                {
                    Random random = new Random();
                    User user = new User { Id = random.Next(0, 25565), Username = Username, Password = Password, Age = Age };
                    unitOfWork.UserRepository.Insert(user);
                    unitOfWork.Save();
                    return View("Login");
                }
                ModelState.AddModelError("error", "User already registered!");
                return View();
            }
            ModelState.AddModelError("error", "Passwords do not mach!");
            return View();
        }

        public ActionResult Logout()
        {
            if (Session["User"] != null)
            {
                Session.Abandon();
            }
            return RedirectToAction("Login");
        }
    }
}