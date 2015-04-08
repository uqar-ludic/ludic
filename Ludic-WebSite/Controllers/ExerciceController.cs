using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ludic_website.Controllers {
  public class ExerciceController : Controller {
    //
    // GET: /Exercice/
    
    public ActionResult List() {
      return View();
    }
  
    public ActionResult Console() {
      return View();
    }

    public ActionResult Score() {
      return View();
    }

    //
    // GET: /Exercice/Details/5

    public ActionResult Details(int id) {
      return View();
    }

    //
    // GET: /Exercice/Create

    public ActionResult Create() {
      return View();
    }

    //
    // POST: /Exercice/Create

    [HttpPost]
    public ActionResult Create(FormCollection collection) {
      try {
        // TODO: Add insert logic here

        return RedirectToAction("Index");
      } catch {
        return View();
      }
    }

    //
    // GET: /Exercice/Edit/5

    public ActionResult Edit(int id) {
      return View();
    }

    //
    // POST: /Exercice/Edit/5

    [HttpPost]
    public ActionResult Edit(int id, FormCollection collection) {
      try {
        // TODO: Add update logic here

        return RedirectToAction("Index");
      } catch {
        return View();
      }
    }

    //
    // GET: /Exercice/Delete/5

    public ActionResult Delete(int id) {
      return View();
    }

    //
    // POST: /Exercice/Delete/5

    [HttpPost]
    public ActionResult Delete(int id, FormCollection collection) {
      try {
        // TODO: Add delete logic here

        return RedirectToAction("Index");
      } catch {
        return View();
      }
    }
  }
}
