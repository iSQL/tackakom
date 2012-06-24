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
    public class EventCategoriesController : Controller
    {
        private DbTackakom db = new DbTackakom();

        //
        // GET: /EventCategories/

        public ViewResult Index()
        {
            return View(db.EventCategories.ToList());
        }

        //
        // GET: /EventCategories/Details/5

        public ViewResult Details(int id)
        {
            EventCategory eventcategory = db.EventCategories.Find(id);
            return View(eventcategory);
        }

        //
        // GET: /EventCategories/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /EventCategories/Create

        [HttpPost]
        public ActionResult Create(EventCategory eventcategory)
        {
            if (ModelState.IsValid)
            {
                db.EventCategories.Add(eventcategory);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(eventcategory);
        }
        
        //
        // GET: /EventCategories/Edit/5
 
        public ActionResult Edit(int id)
        {
            EventCategory eventcategory = db.EventCategories.Find(id);
            return View(eventcategory);
        }

        //
        // POST: /EventCategories/Edit/5

        [HttpPost]
        public ActionResult Edit(EventCategory eventcategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eventcategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(eventcategory);
        }

        //
        // GET: /EventCategories/Delete/5
 
        public ActionResult Delete(int id)
        {
            EventCategory eventcategory = db.EventCategories.Find(id);
            return View(eventcategory);
        }

        //
        // POST: /EventCategories/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            EventCategory eventcategory = db.EventCategories.Find(id);
            db.EventCategories.Remove(eventcategory);
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