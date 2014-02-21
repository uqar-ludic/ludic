using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SiteSandBox.Models;

namespace SiteSandBox.Controllers
{
    public class CommentaireController : Controller
    {
        private SandBoxContext db = new SandBoxContext();

        //
        // GET: /Commentaire/

        public ActionResult Index()
        {
            var commentaires = db.Commentaires.Include(c => c.Exercice);
            return View(commentaires.ToList());
        }

        //
        // GET: /Commentaire/Details/5

        public ActionResult Details(int id = 0)
        {
            Commentaire commentaire = db.Commentaires.Find(id);
            if (commentaire == null)
            {
                return HttpNotFound();
            }
            return View(commentaire);
        }

        //
        // GET: /Commentaire/Create

        public ActionResult Create()
        {
            ViewBag.ExerciceId = new SelectList(db.Exercices, "ExerciceId", "Name");
            return View();
        }

        //
        // POST: /Commentaire/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Commentaire commentaire)
        {
            if (ModelState.IsValid)
            {
                db.Commentaires.Add(commentaire);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ExerciceId = new SelectList(db.Exercices, "ExerciceId", "Name", commentaire.ExerciceId);
            return View(commentaire);
        }

        //
        // GET: /Commentaire/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Commentaire commentaire = db.Commentaires.Find(id);
            if (commentaire == null)
            {
                return HttpNotFound();
            }
            ViewBag.ExerciceId = new SelectList(db.Exercices, "ExerciceId", "Name", commentaire.ExerciceId);
            return View(commentaire);
        }

        //
        // POST: /Commentaire/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Commentaire commentaire)
        {
            if (ModelState.IsValid)
            {
                db.Entry(commentaire).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ExerciceId = new SelectList(db.Exercices, "ExerciceId", "Name", commentaire.ExerciceId);
            return View(commentaire);
        }

        //
        // GET: /Commentaire/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Commentaire commentaire = db.Commentaires.Find(id);
            if (commentaire == null)
            {
                return HttpNotFound();
            }
            return View(commentaire);
        }

        //
        // POST: /Commentaire/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Commentaire commentaire = db.Commentaires.Find(id);
            db.Commentaires.Remove(commentaire);
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