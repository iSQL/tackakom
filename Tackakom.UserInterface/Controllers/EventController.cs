using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tackakom.Model;
using Tackakom.Repository;
using Valentica.Libraries;

namespace Tackakom.UserInterface.Controllers
{ 
    public class EventController : Controller
    {
        private DbTackakom db = new DbTackakom();

        //
        // GET: /Event/

        
        public ActionResult Index(int page = 1)
        {
            //int page = pages.GetValueOrDefault(1);
            var total = db.Events.Select(p => p.Id).Count();
            const int pageSize = 4;
            var skip = pageSize * (page - 1);
            //popunjavanje liste sa modelima
            List<Event> eventi = db.Events
                .OrderBy(x => x.CreateTime)
                .Skip(skip)
                .Take(pageSize)
                .ToList();
            
            Pagination pagination = new Pagination();

            pagination.BaseUrl = "/eventlist/";
            pagination.TotalRows = total;
            pagination.CurPage = page;
            pagination.PerPage = pageSize;

            string pageLinks = pagination.GetPageLinks();
            ViewData["pageLinks"] = pageLinks;
            return View(eventi);
        }

        [Authorize]
        public ActionResult Editing(int page = 1)
        {
            //int page = pages.GetValueOrDefault(1);
            var total = db.Events.Select(p => p.Id).Count();
            const int pageSize = 4;
            var skip = pageSize * (page - 1);
            //popunjavanje liste sa modelima
            List<Event> eventi = db.Events
                .OrderBy(x => x.CreateTime)
                .Skip(skip)
                .Take(pageSize)
                .ToList();

            Pagination pagination = new Pagination();

            pagination.BaseUrl = "Event/Edit/";
            pagination.TotalRows = total;
            pagination.CurPage = page;
            pagination.PerPage = pageSize;

            string pageLinks = pagination.GetPageLinks();
            ViewData["pageLinks"] = pageLinks;
            return View(eventi);
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

        [HttpPost]
        public ActionResult Save(Event _event)
        {
            
            if (ModelState.IsValid)
            {
                db.Events.Add(_event);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
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