using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SickFriday;
using SickFriday.Controllers;
using SickFriday.Models;
using NUnit.Framework;

namespace SickFriday.Tests.Controllers
{
    [TestClass]
    public class SickFridayControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            NUnit.Framework.Assert.IsNotNull(result);
        }

        [TestMethod]
        public void About()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            NUnit.Framework.Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }

        [TestMethod]
        public void Contact()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            NUnit.Framework.Assert.IsNotNull(result);
        }
        [TestMethod]
        public void Member()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Member() as ViewResult;

            // Assert
            NUnit.Framework.Assert.IsNotNull(result);
        }

       
        [TestMethod]
        public void CreateRanking_ReturnViewResult()
        {
            // Arrange
            RankingController controller = new RankingController();


            // Act
            //var result = controller.Index();
            ViewResult result = controller.Create() as ViewResult;

            // Assert
            NUnit.Framework.Assert.IsNotNull(result);

            //Assert.IsInstanceOfType(result,ViewResult);
            //Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CreateUser_ReturnViewResult()
        {
            // Arrange
            UserController controller = new UserController();

            // Act
            //var result = controller.Index();
            ViewResult result = controller.Create() as ViewResult;

            // Assert
            NUnit.Framework.Assert.IsNotNull(result);

            //Assert.IsInstanceOfType(result,ViewResult);
            //Assert.IsNotNull(result);
        }

    }

}
