using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Tackakom.Model;
using Tackakom.Repository;
using Valentica.Libraries;

namespace Tackakom.UserInterface.Controllers
{ 
    public class HostController : Controller
    {
        private DbTackakom db = new DbTackakom();

        public ActionResult Index(int hostId, int page = 1)
        {
            var total = db.Events.Select(x => x.Host.Id).Where(x => x.Equals(hostId)).Count();
            const int pageSize = 4;
            var skip = pageSize * (page - 1);
            List<Event> places = db.Events.Where(x => x.Host.Id.Equals(hostId)).OrderBy(x => x.StartDate).Skip(skip).Take(pageSize).ToList();
            var pagination = new Pagination
                                        {
                                            BaseUrl = "/places/" + "/hostId=" + hostId.ToString() + "/",
                                            TotalRows = total,
                                            CurPage = page,
                                            PerPage = pageSize
                                        };

            ViewData["pageLinks"] = pagination.GetPageLinks(); ;
            ViewData["placeName"] = db.Hosts.Find(hostId).Name;
            return View(places);
        }


        //Pojedinacni host
        public ViewResult SingleHost(int id)
        {
            throw new NotImplementedException("Funkcija nije generisana");
        }

        public ActionResult Create()
        {     
                return View();
        } 

        [HttpPost][Authorize]
        public ActionResult Create(Host host)
        {
            //Vezivanje Guid-a usera za host userId
            var membershipUser = Membership.GetUser();
            if (membershipUser != null)
            {
                var providerUserKey = membershipUser.ProviderUserKey;
                if (providerUserKey != null)
                {
                    var guid = (Guid)providerUserKey;
                    host.UserID = guid;
                }
            }

            if (ModelState.IsValid)
            {
                db.Hosts.Add(host);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");  
            }

            return View(host);
        }
        
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}