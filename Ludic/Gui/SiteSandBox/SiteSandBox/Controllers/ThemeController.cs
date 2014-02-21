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
    public class ThemeController : Controller
    {
        private SandBoxContext db = new SandBoxContext();

        //
        // GET: /Theme/

        public ActionResult Index()
        {
            return View(db.Themes.ToList());
        }

        //
        // GET: /Theme/Details/5

        public ActionResult Details(int id = 0)
        {
            Theme theme = db.Themes.Find(id);
            if (theme == null)
            {
                return HttpNotFound();
            }
            return View(theme);
        }

        //
        // GET: /Theme/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Theme/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Theme theme)
        {
            if (ModelState.IsValid)
            {
                db.Themes.Add(theme);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(theme);
        }

        //
        // GET: /Theme/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Theme theme = db.Themes.Find(id);
            if (theme == null)
            {
                return HttpNotFound();
            }
            return View(theme);
        }

        //
        // POST: /Theme/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Theme theme)
        {
            if (ModelState.IsValid)
            {
                db.Entry(theme).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(theme);
        }

        //
        // GET: /Theme/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Theme theme = db.Themes.Find(id);
            if (theme == null)
            {
                return HttpNotFound();
            }
            return View(theme);
        }

        //
        // POST: /Theme/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Theme theme = db.Themes.Find(id);
            db.Themes.Remove(theme);
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