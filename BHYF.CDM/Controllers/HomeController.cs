using BHYF.CDM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BHYF.CDM.Controllers
{
    public class HomeController : Controller
    {
        YwtDbContext ctx;

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

    }
}
