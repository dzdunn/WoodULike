using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WoodULike.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Why did we start this site?";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Get in touch if you'd like to suggest or commission a new project!";

            return View();
        }
    }
}