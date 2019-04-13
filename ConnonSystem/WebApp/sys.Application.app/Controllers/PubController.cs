using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sys.Aplication.Code;
using sys.Application.app.Controllers.Extensions;
using sys.Dal.Busines.AuthorizeManage;
using sys.Dal.Busines.BaseManage;
using sys.Dal.Busines.SystemManage;
using sys.Dal.Entity.BaseManage;
using sys.Dal.Entity.SystemManage;
using sys.Util;
using sys.Util.Attributes;
using sys.Util.WebControl;

namespace sys.Application.app.Controllers
{
    public class PubController : Controller
    {
        // GET: Home
        public ActionResult Index()
        { 
            return View();
        }
        public ActionResult MuiScan()
        { 
            return View();
        }
        public ActionResult H5Scan()
        { 
            return View();
        } 
        public ActionResult WhtScan()
        {

            return View();
        }
        public ActionResult PCScan()
        {

            return View();
        }
        
    }
}