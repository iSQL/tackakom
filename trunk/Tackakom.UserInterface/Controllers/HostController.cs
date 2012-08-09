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
    public class HostController : Controller
    {
        private DbTackakom db = new DbTackakom();

        //
        // GET: /Host/

        public ViewResult Index()
        {
            return View(db.Hosts.ToList());
        }

        //
        // GET: /Host/Details/5

        public ViewResult Details(int id)
        {
            Host host = db.Hosts.Find(id);
            return View(host);
        }

        //
        // GET: /Host/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Host/Create

        [HttpPost]
        public ActionResult Create(Host host)
        {
            if (ModelState.IsValid)
            {
                db.Hosts.Add(host);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(host);
        }
        
        //
        // GET: /Host/Edit/5
 
        public ActionResult Edit(int id)
        {
            Host host = db.Hosts.Find(id);
            return View(host);
        }

        //
        // POST: /Host/Edit/5

        [HttpPost]
        public ActionResult Edit(Host host)
        {
            if (ModelState.IsValid)
            {
                db.Entry(host).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(host);
        }

        //
        // GET: /Host/Delete/5
 
        public ActionResult Delete(int id)
        {
            Host host = db.Hosts.Find(id);
            return View(host);
        }

        //
        // POST: /Host/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Host host = db.Hosts.Find(id);
            db.Hosts.Remove(host);
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