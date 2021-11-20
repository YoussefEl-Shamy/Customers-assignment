using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Customers.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Customers");
        }

        public ActionResult About()
        {
            ViewBag.Message = "I'm Youssef Mohamed Mostafa, Junior Software Developer, B. SC. in Computer Science.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Youssef Mohamed contact page.";

            return View();
        }
    }
}