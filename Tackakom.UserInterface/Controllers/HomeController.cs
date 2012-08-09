using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Tackakom.UserInterface.Controllers
{
    public class HomeController : Controller
    {

        
        public ActionResult Index()
        {
           // System.Web.HttpContext.Current.User.Identity.
           // Guid guid = (Guid)Membership.GetUser().ProviderUserKey;
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Single()
        {
            return View();
        }
    }
}
