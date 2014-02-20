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
            ExerciceConsole em = new ExerciceConsole();
            em.IdExercice = id;
            return View(em);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ExerciceConsole exercice)
        {
            switch (exercice.Action)
            {
                case ExerciceConsole.Actions.SAVE:
                    Save(exercice);
                    break;
                case ExerciceConsole.Actions.COMMENTER:
                    exercice = Comment(exercice);
                    break;
                case ExerciceConsole.Actions.COMPILE:
                    exercice = Compile(exercice);
                    break;
            }
            return View(exercice);
        }

        private void Save(ExerciceConsole exercice)
        {

        }

        private ExerciceConsole Comment(ExerciceConsole exercice)
        {
            if (!String.IsNullOrEmpty(exercice.Posted.Title) && !String.IsNullOrEmpty(exercice.Posted.Comment))
            {
                exercice.Comments.Add(new CommentaireConsole(exercice.Comments.Count(), User.Identity.Name, DateTime.Now, exercice.Posted.Title, exercice.Posted.Comment));
                exercice.Posted = new CommentaireConsole();
            }
            return exercice;
        }

        private ExerciceConsole Compile(ExerciceConsole exercice)
        {
            return exercice;
        }

    }
}
