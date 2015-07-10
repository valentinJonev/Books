using System;
using BooksUI.Controllers;
using Model.Models;
using System.Web;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BooksUI.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void LoginWithNoUsersRegisteredFail()
        {
            string username = "testUsername";
            string password = "testPassword";
            var controller = new UserController();

            var result = controller.Login(username, password) as ViewResult;

            Assert.AreEqual(result.ViewName, "Login");
        }

        [TestMethod]
        public void AsksForIndexView()
        {
            // Arrange
            var controller = new HomeController();
            // Act
            ViewResult result = controller.Index() as ViewResult;
            // Assert
            Assert.AreEqual("Index", result.ViewName);
        } 
    }
}
