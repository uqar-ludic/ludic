using SiteSandBox.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SiteSandBox.Controllers
{
    public class ConsoleController : Controller
    {
        //private SandBoxContext db = new SandBoxContext();

        //
        // GET: /Edition/

        public ActionResult Edit(int id = 0)
        {
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
            ExerciceConsole em = new ExerciceConsole();
            em.OutCompil = "";
            em.OutSuccess = "";
            if (id == 0)
                em = init();
            return View(em);
        }

        private ExerciceConsole init()
        {
            ExerciceConsole em = new ExerciceConsole();
            em.IdExercice = 0;
            em.Value = 5;
            em.Subject = "SALUT LA COMPAGNIE";
            em.Success.Add(new SuccessConsole(1, SuccessConsole.Difficulty.EASY, "test1", "succes n°1", true));
            em.Success.Add(new SuccessConsole(2, SuccessConsole.Difficulty.EASY, "test2", "succes n°2", true));
            em.Success.Add(new SuccessConsole(3, SuccessConsole.Difficulty.MEDIUM, "test3", "succes n°3"));
            em.Success.Add(new SuccessConsole(4, SuccessConsole.Difficulty.HARD, "test4", "succes n°4"));
            em.Errors.Add(new ErrorConsole(1, 125, "error on ..."));
            em.Errors.Add(new ErrorConsole(2, 142, "error on ..."));
            em.Errors.Add(new ErrorConsole(3, 840, "error on ..."));
            em.Errors.Add(new ErrorConsole(4, 1985, "error on ..."));
            return em;
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
            Console.WriteLine(exercice.Code);
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
            SiteSandBoxDemo.localhost.LudicWebService service = new SiteSandBoxDemo.localhost.LudicWebService();
            exercice.OutCompil = service.CompilSln(@"C:\Temp\AQGPI\AQGPI\AQGPI.sln", @"C:\Logs\AQGPI.log.txt");
            exercice.OutSuccess = System.Environment.NewLine + service.Execute(@"C:\Temp\AQGPI\AQGPI\AQGPI\bin\Debug\Perm.txt", @"C:\Temp\AQGPI\AQGPI\AQGPI\bin\Debug\AQGPI.exe");
            return exercice;
        }

    }
}
