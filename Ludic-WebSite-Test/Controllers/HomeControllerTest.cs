using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ludic_website;
using Ludic_website.Controllers;

namespace Ludic_website.Tests.Controllers {
  [TestClass]
  public class HomeControllerTest {
    [TestMethod]
    public void Index() {
      // Arrange
      HomeController controller = new HomeController();

      // Act
      ViewResult result = controller.Index() as ViewResult;

      // Assert
      Assert.AreEqual("Modify this template to jump-start your ASP.NET MVC application.", result.ViewBag.Message);
    }

    [TestMethod]
    public void About() {
      // Arrange
      HomeController controller = new HomeController();

      // Act
      ViewResult result = controller.About() as ViewResult;

      // Assert
      Assert.IsNotNull(result);
    }

    [TestMethod]
    public void Contact() {
      // Arrange
      HomeController controller = new HomeController();

      // Act
      ViewResult result = controller.Contact() as ViewResult;

      // Assert
      Assert.IsNotNull(result);
    }
  }

  [TestClass]
  public class ExerciceControllerTest {
    [TestMethod]
    public void List() {
      ExerciceController controller = new ExerciceController();
      ViewResult result = controller.List() as ViewResult;
      Assert.IsNotNull(result);
    }

    [TestMethod]
    public void Console() {
      ExerciceController controller = new ExerciceController();
      ViewResult result = controller.Console() as ViewResult;
      Assert.IsNotNull(result);
    }

    [TestMethod]
    public void Score() {
      ExerciceController controller = new ExerciceController();
      ViewResult result = controller.Score() as ViewResult;
      Assert.IsNotNull(result);
    }
  }
}
