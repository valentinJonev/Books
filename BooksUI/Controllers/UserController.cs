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
            try
            {
                var user = this.unitOfWork.UserRepository.Get(u => u.Username == Username && u.Password == Password);
                if (user.Any())
                {
                    this.Session["User"] = user.First();
                    return this.RedirectToAction(MVC.Home.Index());
                }

                ModelState.AddModelError("error", "Invalid username or password!");
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                throw;
            }
            
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
            try
            {
                if (Password == RepeatPassword)
                {
                    if (!this.unitOfWork.UserRepository.Get(u => u.Username == Username).Any())
                    {
                        User user = new User { Username = Username, Password = Password, Age = Age };
                        this.unitOfWork.UserRepository.Insert(user);
                        this.unitOfWork.Save();
                        return this.RedirectToAction(MVC.Home.Index());
                    }

                    ModelState.AddModelError("error", "User already registered!");
                    return this.View();
                }

                ModelState.AddModelError("error", "Passwords do not mach!");
                return this.View();
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                throw;
            }
        }

        public virtual ActionResult Logout()
        {
            try
            {
                if (this.Session["User"] != null)
                {
                    Session.Abandon();
                    this.unitOfWork.Dispose();
                }

                return this.RedirectToAction(MVC.User.Login());
            }
            catch (Exception ex)
            {
                Log.LogError(ex);
                throw;
            }
        }
    }
}