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
    public class EventController : Controller
    {
        private DbTackakom db = new DbTackakom();

        //
        // GET: /Event/

        public ViewResult Index()
        {
            return View(db.Events.ToList());
        }

        //
        // GET: /Event/Details/5

        public ViewResult Details(int id)
        {
            Event _event = db.Events.Find(id);
            return View(_event);
        }

        //
        // GET: /Event/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Event/Create

        [HttpPost]
        public ActionResult Create(Event _event)
        {
            if (ModelState.IsValid)
            {
                db.Events.Add(_event);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(_event);
        }
        
        //
        // GET: /Event/Edit/5
 
        public ActionResult Edit(int id)
        {
            Event _event = db.Events.Find(id);
            return View(_event);
        }

        //
        // POST: /Event/Edit/5

        [HttpPost]
        public ActionResult Edit(Event _event)
        {
            if (ModelState.IsValid)
            {
                db.Entry(_event).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(_event);
        }

        //
        // GET: /Event/Delete/5
 
        public ActionResult Delete(int id)
        {
            Event _event = db.Events.Find(id);
            return View(_event);
        }

        //
        // POST: /Event/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Event _event = db.Events.Find(id);
            db.Events.Remove(_event);
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