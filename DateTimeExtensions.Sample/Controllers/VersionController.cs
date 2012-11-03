using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DateTimeExtensions.WorkingDays;

namespace DateTimeExtensions.Sample.Controllers
{
    public class VersionController : Controller
    {
        //
        // GET: /Version/

        public ActionResult Index()
        {
            var assembly = typeof (IWorkingDayCultureInfo).Assembly;
            ViewBag.Version = assembly.GetName().Version;
            return PartialView();
        }

    }
}
