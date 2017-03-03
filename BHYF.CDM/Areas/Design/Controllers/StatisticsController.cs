using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BHYF.CDM.Areas.Design.Controllers
{
    public class StatisticsController : Controller
    {
        //
        // GET: /Design/Statistics/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Hot()
        {
            BHYF.CDM.Controllers.DesignController control = new CDM.Controllers.DesignController();
            var msg = control.Get();
            return View();
        }

        public ActionResult Details()
        {
            return View();
        }

        public ActionResult SignUp()
        {
            return View();
        }
        public ActionResult Acivity()
        {
            return View();
        }
	}
}