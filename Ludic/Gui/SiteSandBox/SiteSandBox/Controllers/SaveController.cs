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
    public class SaveController : Controller
    {
        private SandBoxContext db = new SandBoxContext();

        //
        // GET: /Save/

        public ActionResult Index()
        {
            return View(db.Saves.ToList());
        }

        //
        // GET: /Save/Details/5

        public ActionResult Details(int id = 0)
        {
            Save save = db.Saves.Find(id);
            if (save == null)
            {
                return HttpNotFound();
            }
            return View(save);
        }

        //
        // GET: /Save/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Save/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Save save)
        {
            if (ModelState.IsValid)
            {
                db.Saves.Add(save);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(save);
        }

        //
        // GET: /Save/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Save save = db.Saves.Find(id);
            if (save == null)
            {
                return HttpNotFound();
            }
            return View(save);
        }

        //
        // POST: /Save/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Save save)
        {
            if (ModelState.IsValid)
            {
                db.Entry(save).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(save);
        }

        //
        // GET: /Save/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Save save = db.Saves.Find(id);
            if (save == null)
            {
                return HttpNotFound();
            }
            return View(save);
        }

        //
        // POST: /Save/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Save save = db.Saves.Find(id);
            db.Saves.Remove(save);
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