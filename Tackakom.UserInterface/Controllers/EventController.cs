using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Objects;
using System.Globalization;
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

        public ActionResult Index(int page = 1)
        {
            var total = db.Events.Select(p => p.Id).Count();
            const int pageSize = 4;
            var skip = pageSize * (page - 1);
            List<Event> eventi = db.Events.OrderBy(x => x.StartDate).Skip(skip).Take(pageSize).ToList();

            var pagination = new Pagination
                                        {BaseUrl = "/events/", TotalRows = total, CurPage = page, PerPage = pageSize};

            ViewData["pageLinks"] = pagination.GetPageLinks(); 
            return View(eventi);
        }

        public ActionResult GetPricelessEvents(int page = 1)
        {
            var total = db.Events.Select(p => p.Entry).Count(p=>p.Equals("Slobodno"));
            const int pageSize = 4;
            var skip = pageSize * (page - 1);
            List<Event> eventi = db.Events.OrderBy(x => x.StartDate).Where(x => x.Entry.Equals("Slobodno")).Skip(skip).Take(pageSize).ToList();

            var pagination = new Pagination { BaseUrl = "/priceless/", TotalRows = total, CurPage = page, PerPage = pageSize };

            ViewData["pageLinks"] = pagination.GetPageLinks();
            return View("Index", eventi);
        }

        //Ovo treba protestirati
        public ActionResult GetEventsByDate(string date, int page = 1)
        {
            DateTime dateR;
            DateTime.TryParse(date, CultureInfo.CreateSpecificCulture("en-US"), DateTimeStyles.None, out dateR);
            var total = db.Events.Select(x => x.StartDate).Count(x => x.Equals(dateR));
            const int pageSize = 4;
            var skip = pageSize * (page - 1);

            List<Event> eventi = db.Events.OrderBy(x => x.StartDate).Where(x => x.StartDate.Equals(dateR)).Skip(skip).Take(pageSize).ToList();

            var pagination = new Pagination
                                        {
                                            BaseUrl = "/date/" + date + "/",
                                            TotalRows = total,
                                            CurPage = page,
                                            PerPage = pageSize
                                        };
            ViewData["pageLinks"] = pagination.GetPageLinks();
            return View("Index",eventi);
        }

        public ActionResult GetEventsByName(string name, int page = 1)
        {
            var total = db.Events.Count(x => x.Title.Contains(name));
            const int pageSize = 4;
            var skip = pageSize * (page - 1);
            List<Event> eventi = new List<Event>();
            foreach (Event @event in db.Events.OrderBy(x => x.StartDate).Where(x => x.Title.Contains(name)).Skip(skip).Take(pageSize))
                eventi.Add(@event);

            if (total==1)
            {
                Event singleEvent = eventi[0];
                return View("SingleEvent", singleEvent);
            }
            else
            {
                var pagination = new Pagination
                                     {
                                         BaseUrl = "/name/" + name + "/",
                                         TotalRows = total,
                                         CurPage = page,
                                         PerPage = pageSize
                                     };

                ViewData["pageLinks"] = pagination.GetPageLinks();
                return View("Index",eventi);
            }
        }

        public ActionResult GetEventsByPlace(string place, int page = 1)
        {
            var total = db.Events.Count(x => x.Host.Name.Contains(place));
            const int pageSize = 4;
            var skip = pageSize * (page - 1);
            List<Event> eventi = db.Events.OrderBy(x => x.StartDate).Where(x => x.Host.Name.Contains(place)).Skip(skip).Take(pageSize).ToList();

            var pagination = new Pagination
                                 {
                                     BaseUrl = "/place/" + place + "/",
                                     TotalRows = total,
                                     CurPage = page,
                                     PerPage = pageSize
                                 };
                ViewData["pageLinks"] = pagination.GetPageLinks();
                return View("Index", eventi);
        }


        [Authorize]
        public ActionResult Editing(int page = 1)
        {
            Host host = null;
            var membershipUser = Membership.GetUser();
            if (membershipUser != null)
            {
                var providerUserKey = membershipUser.ProviderUserKey;
                if (providerUserKey != null)
                {
                    //Za hosta ubaci trenutnog hosta
                     host = db.Hosts.Single(x => x.UserID.Equals((Guid)providerUserKey));
                }
            }
            ViewBag.Korisnik = host.Name;
            var total = db.Events.Select(x=>x.Host.Id).Count(x => x.Equals(host.Id));
            const int pageSize = 4;
            var skip = pageSize * (page - 1);
            List<Event> eventi = db.Events.OrderBy(x => x.StartDate).Where(x => x.Host.Id.Equals(host.Id)).Skip(skip).Take(pageSize).ToList();
            var pagination = new Pagination
                                        {BaseUrl = "/my_events/", TotalRows = total, CurPage = page, PerPage = pageSize};

            ViewData["pageLinks"] = pagination.GetPageLinks();
            return View(eventi);
        }

        //Pregled pojedinacnog eventa
        public ViewResult SingleEvent(int id)
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
                    //Za hosta ubaci trenutno ulogovanog hosta
                    _event.Host = db.Hosts.Single(host => host.UserID.Equals((Guid) providerUserKey));
                }
            }
            //Pronadji kategoriju sa zadatim ID
            _event.EventCategory = db.EventCategories.Single(r => r.Id.Equals(_event.EventCategory.Id));
            
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
            return Content("Nije ok");
        }

        //Vraca naslove svih dogadjaja koji zadovoljavaju uslov
        public JsonResult GetEventTitles(string term)
        {
            var eventNames = db.Events.OrderBy(x => x.StartDate).Where(x => x.Title.Contains(term)).Select(x => x.Title).ToArray();
            return Json(eventNames, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPlaceTitles(string term)
        {
            var placeNames = db.Events.OrderBy(x => x.StartDate).Where(x => x.Host.Name.Contains(term)).Select(x => x.Host.Name).ToArray();
            return Json(placeNames, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}