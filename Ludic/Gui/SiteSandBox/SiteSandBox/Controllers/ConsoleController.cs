using SiteSandBox.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SiteSandBox.Controllers
{
    public class ConsoleController : Controller
    {
        private SandBoxContext db = new SandBoxContext();

        //
        // GET: /Edition/

        public ActionResult Edit(int id = 0)
        {
            ExerciceModel em = new ExerciceModel();
            em.IdExercice = id;
            return View(em);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ExerciceModel exercice)
        {
            return View(exercice);
        }



    }
}
