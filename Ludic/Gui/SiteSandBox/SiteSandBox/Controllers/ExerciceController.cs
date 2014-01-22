using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SiteSandBox.Models;
using WebMatrix.WebData;

namespace SiteSandBox.Controllers
{
    public class ExerciceController : Controller
    {
        private SandBoxContext db = new SandBoxContext();

        //
        // GET: /Exercice/

        public ActionResult Index()
        {
            var exercices = db.Exercices.Include(e => e.Theme);
            int id = WebSecurity.GetUserId(User.Identity.Name);
            Historique h = db.Historiques.FirstOrDefault(u => u.UserId == id);
            ViewBag.LastExercice = h.LastExerciceId;
            ViewBag.PrevExercice = h.LastExerciceId - 1;
            Exercice tmp = exercices.ToList().LastOrDefault();
            if (tmp != null)
                if (tmp.ExerciceId <= h.LastExerciceId + 1)
                    ViewBag.NextExercice = h.LastExerciceId + 1;
                else
                    ViewBag.NextExercice = -1;
            else
                ViewBag.NextExercice = -1;
            return View(exercices.ToList());
        }

        //
        // GET: /Exercice/Details/5

        public ActionResult Details(int id = 0)
        {
            Exercice exercice = db.Exercices.Find(id);
            if (exercice == null)
            {
                return HttpNotFound();
            }
            return View(exercice);
        }

        //
        // GET: /Exercice/Create

        public ActionResult Create()
        {
            ViewBag.ThemeId = new SelectList(db.Themes, "ThemeId", "Name");
            return View();
        }

        //
        // POST: /Exercice/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Exercice exercice)
        {
            if (ModelState.IsValid)
            {
                db.Exercices.Add(exercice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ThemeId = new SelectList(db.Themes, "ThemeId", "Name", exercice.ThemeId);
            return View(exercice);
        }

        //
        // GET: /Exercice/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Exercice exercice = db.Exercices.Find(id);
            if (exercice == null)
            {
                return HttpNotFound();
            }
            ViewBag.ThemeId = new SelectList(db.Themes, "ThemeId", "Name", exercice.ThemeId);
            return View(exercice);
        }

        //
        // POST: /Exercice/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Exercice exercice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(exercice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ThemeId = new SelectList(db.Themes, "ThemeId", "Name", exercice.ThemeId);
            return View(exercice);
        }

        //
        // GET: /Exercice/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Exercice exercice = db.Exercices.Find(id);
            if (exercice == null)
            {
                return HttpNotFound();
            }
            return View(exercice);
        }

        //
        // POST: /Exercice/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Exercice exercice = db.Exercices.Find(id);
            db.Exercices.Remove(exercice);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}