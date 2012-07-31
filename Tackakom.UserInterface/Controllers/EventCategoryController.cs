﻿using System;
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
    public class EventCategoryController : Controller
    {
        private DbTackakom db = new DbTackakom();

        //
        // GET: /EventCategory/

        public ViewResult Index()
        {
            return View(db.EventCategories.ToList());
        }

        //
        // GET: /EventCategory/Details/5

        public ViewResult Details(int id)
        {
            EventCategory eventcategory = db.EventCategories.Find(id);
            return View(eventcategory);
        }

        //
        // GET: /EventCategory/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /EventCategory/Create

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
        // GET: /EventCategory/Edit/5
 
        public ActionResult Edit(int id)
        {
            EventCategory eventcategory = db.EventCategories.Find(id);
            return View(eventcategory);
        }

        //
        // POST: /EventCategory/Edit/5

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
        // GET: /EventCategory/Delete/5
 
        public ActionResult Delete(int id)
        {
            EventCategory eventcategory = db.EventCategories.Find(id);
            return View(eventcategory);
        }

        //
        // POST: /EventCategory/Delete/5

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