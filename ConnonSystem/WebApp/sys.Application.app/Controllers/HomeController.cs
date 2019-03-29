using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sys.Util;

namespace sys.Application.app.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            ReturnResult rst = CCPSMSNotifyUtil.TestNotify("18708508278", "2222", "3");
            //ReturnResult rst1 = CCPSMSNotifyUtil.TestNotify("15208547251","2222","3");
            return View();
        }
    }
}