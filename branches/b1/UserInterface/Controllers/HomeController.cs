using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UserInterface.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Testiranje MVC-a";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
