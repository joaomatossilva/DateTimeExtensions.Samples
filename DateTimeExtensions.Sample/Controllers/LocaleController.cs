using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DateTimeExtensions.Sample.Controllers
{
    public class LocaleController : Controller
    {
        //
        // GET: /Locale/

        public ActionResult Index() {
        	ViewBag.Locale = System.Threading.Thread.CurrentThread.CurrentCulture.Name;
            return PartialView();
        }

    }
}
