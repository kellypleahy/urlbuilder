using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcTestApp.Controllers
{
    public class ViewPage1Controller : Controller
    {
        public ActionResult Index()
        {
            return View(new object());
        }

        public ViewResult Action1(int foo)
        {
            return View();
        }
    }
}
