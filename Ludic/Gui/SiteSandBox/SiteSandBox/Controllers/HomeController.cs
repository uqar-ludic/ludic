using SiteSandBox.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace SiteSandBox.Controllers
{
    public class HomeController : Controller
    {
        private SandBoxContext db = new SandBoxContext();

        [AllowAnonymous]
        public ActionResult Index()
        {
            ViewBag.Message = "Bienvenu sur SandBox";
            return View();
        }

        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
