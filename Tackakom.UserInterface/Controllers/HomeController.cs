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
            return RedirectToAction("Index","Event", new { page = 1 });
        }
    }
}
