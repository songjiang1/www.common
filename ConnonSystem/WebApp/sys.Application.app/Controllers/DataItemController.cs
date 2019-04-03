using sys.Dal.Busines.BaseManage;
using sys.Dal.Busines.SystemManage;
using sys.Dal.Cache;
using sys.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sys.Application.app.Controllers
{
    public class DataItemController : Controller
    {
        private DataItemDetailBLL dataItemDetailBLL = new DataItemDetailBLL();
        private DataItemCache dataItemCache = new DataItemCache();
        private OrganizeBLL organizeBLL = new OrganizeBLL();
        // GET: DataItem
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 获取数据字典列表（绑定控件）
        /// </summary>
        /// <param name="EnCode">代码</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetDataItemListJson(string EnCode)
        {
            var data = dataItemCache.GetDataItemList(EnCode);
            return Content(data.ToJson());
        }
        [HttpGet]
        public ActionResult GetOrganizeJson(string EnCode)
        {
            var data = organizeBLL.GetList();
            return Content(data.ToJson());
        }
    }
}