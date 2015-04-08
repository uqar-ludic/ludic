using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ludic_website.Controllers {
  public class ErrorsController : Controller {
    //
    // GET: /Errors/

    public ActionResult Unauthorized() {
      return View();
    }

    //
    // GET: /Errors/Details/5

    public ActionResult Details(int id) {
      return View();
    }

    //
    // GET: /Errors/Create

    public ActionResult Create() {
      return View();
    }

    //
    // POST: /Errors/Create

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
    // GET: /Errors/Edit/5

    public ActionResult Edit(int id) {
      return View();
    }

    //
    // POST: /Errors/Edit/5

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
    // GET: /Errors/Delete/5

    public ActionResult Delete(int id) {
      return View();
    }

    //
    // POST: /Errors/Delete/5

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
