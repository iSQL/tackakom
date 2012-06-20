using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tackakom.Model;
using Tackakom.Repository;

namespace Tackakom.UserInterface.Controllers
{ 
    public class IconController : Controller
    {
        private DbTackakom db = new DbTackakom();

        //
        // GET: /Icon/

        public ViewResult Index()
        {
            return View(db.Icons.ToList());
        }

        //
        // GET: /Icon/Details/5

        public ViewResult Details(int id)
        {
            Icon icon = db.Icons.Find(id);
            return View(icon);
        }

        //
        // GET: /Icon/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Icon/Create

        [HttpPost]
        public ActionResult Create(Icon icon)
        {
            if (ModelState.IsValid)
            {
                db.Icons.Add(icon);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(icon);
        }
        
        //
        // GET: /Icon/Edit/5
 
        public ActionResult Edit(int id)
        {
            Icon icon = db.Icons.Find(id);
            return View(icon);
        }

        //
        // POST: /Icon/Edit/5

        [HttpPost]
        public ActionResult Edit(Icon icon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(icon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(icon);
        }

        //
        // GET: /Icon/Delete/5
 
        public ActionResult Delete(int id)
        {
            Icon icon = db.Icons.Find(id);
            return View(icon);
        }

        //
        // POST: /Icon/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Icon icon = db.Icons.Find(id);
            db.Icons.Remove(icon);
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