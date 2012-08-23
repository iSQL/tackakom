using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
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

            pagination.BaseUrl = "/editing/";
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
            Host _host = null;
            var membershipUser = Membership.GetUser();
            if (membershipUser != null)
            {
                var providerUserKey = membershipUser.ProviderUserKey;
                if (providerUserKey != null)
                {
                    //Za hosta ubaci trenutnog hosta
                     _host = db.Hosts.Single(x => x.UserID.Equals((Guid)providerUserKey));
                }
            }
            ViewBag.Korisnik = _host.Name;
            //int page = pages.GetValueOrDefault(1);
            var total = db.Events.Select(x=>x.Host.Id).Where(x=>x.Equals(_host.Id)).Count();
            const int pageSize = 4;
            var skip = pageSize * (page - 1);
            //popunjavanje liste sa modelima
            List<Event> eventi = db.Events
                .OrderBy(x => x.CreateTime)
                .Where(x => x.Host.Id.Equals(_host.Id))
                .Skip(skip)
                .Take(pageSize)
                .ToList();

            Pagination pagination = new Pagination();

            pagination.BaseUrl = "/editing/";
            pagination.TotalRows = total;
            pagination.CurPage = page;
            pagination.PerPage = pageSize;

            string pageLinks = pagination.GetPageLinks();
            ViewData["pageLinks"] = pageLinks;
            return View(eventi);
        }

        //Single
        public ViewResult Details(int id)
        {
            Event _event = db.Events.Find(id);
            return View(_event);
        }


        [HttpPost]
        public ActionResult Save(Event _event)
        {  var membershipUser = Membership.GetUser();
            if (membershipUser != null)
            {
                var providerUserKey = membershipUser.ProviderUserKey;
                if (providerUserKey != null)
                {
                    //Za hosta ubaci trenutnog hosta
                    _event.Host = db.Hosts.Single(host => host.UserID.Equals((Guid) providerUserKey));
                }
            }
            //Pronadji kategoriju sa zadatim ID
            _event.EventCategory = db.EventCategories.Single(r => r.Id.Equals(_event.EventCategory.Id));
            
            //ModelState[]
            if (ModelState.IsValid)
            {
                db.Events.Add(_event);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        //todo: Host moze da brise samo svoje evente
        public ActionResult Brisanje(int id)
        {
            Event _event = db.Events.Find(id);
            db.Events.Remove(_event);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //Edit
        [HttpPost]
        public ActionResult Edit(Event _event)
        {
            var membershipUser = Membership.GetUser();
            if (membershipUser != null)
            {
                var providerUserKey = membershipUser.ProviderUserKey;
                if (providerUserKey != null)
                {
                    //Za hosta ubaci trenutnog hosta
                    _event.Host = db.Hosts.Single(host => host.UserID.Equals((Guid)providerUserKey));
                }
            }
            
            //Pronadji kategoriju sa zadatim ID
            _event.EventCategory = db.EventCategories.Single(r => r.Id.Equals(_event.EventCategory.Id));

            if (ModelState.IsValid)
            {
                Event _eventTemp = db.Events.Find(_event.Id);
                db.Events.Remove(_eventTemp);
                db.Events.Add(_event);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(_event);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}