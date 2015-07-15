namespace BooksUI.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Model.Models;

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
            if (this.Session["User"] != null)
            {
                return this.RedirectToAction(MVC.Home.Index());   
            }

            return this.View();
        }

        [HttpPost]
        public virtual ActionResult Login(string Username, string Password)
        {
            var user = this.unitOfWork.UserRepository.Get(u => u.Username == Username && u.Password == Password);
            if (user.Count() > 0)
            {
                this.Session["User"] = user.First();
                return this.RedirectToAction(MVC.Home.Index());
            }

            ModelState.AddModelError("error", "Invalid username or password!");
            return this.View();
        }

        [HttpGet]
        public virtual ActionResult Register()
        {
            if (this.Session["User"] != null)
            {
                return this.RedirectToAction(MVC.Home.Index());
            }

            return this.View();
        }

        [HttpPost]
        public virtual ActionResult Register(string Username, string Password, string RepeatPassword, int Age)
        {
            if (Password == RepeatPassword)
            {
                if (this.unitOfWork.UserRepository.Get(u => u.Username == Username).Count() == 0)
                {
                    Random random = new Random();
                    User user = new User { Id = random.Next(0, 25565), Username = Username, Password = Password, Age = Age };
                    this.unitOfWork.UserRepository.Insert(user);
                    this.unitOfWork.Save();
                    return this.View(MVC.User.Login());
                }

                ModelState.AddModelError("error", "User already registered!");
                return this.View();
            }

            ModelState.AddModelError("error", "Passwords do not mach!");
            return this.View();
        }

        public virtual ActionResult Logout()
        {
            if (this.Session["User"] != null)
            {
                Session.Abandon();
                this.unitOfWork.Dispose();
            }

            return this.RedirectToAction(MVC.User.Login());
        }
    }
}