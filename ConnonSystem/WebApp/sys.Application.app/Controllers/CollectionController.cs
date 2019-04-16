using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sys.Aplication.Code;
using sys.Application.app.Controllers.Extensions;
using sys.Util;
using sys.Util.WebControl;
using sys.Dal.Busines.AppManage;
using sys.Dal.Busines.TVShowManage;
using System.Text;
using System.IO;
using sys.Util.WeChatSDK;
using sys.Dal.Entity.TVShowManage;
using sys.Application.app.Models;
using sys.Dal.Entity.AppManage;

namespace sys.Application.app.Controllers
{
    public class CollectionController : BaseController
    {

        private CollectionBLL collectionBll = new CollectionBLL();
        private CommentBLL commentBLL = new CommentBLL();
        #region MyRegion 视图
        public ActionResult Index()
        { 
            return View();
        }
        /// <summary>
        /// 双联系
        /// </summary>
        /// <returns></returns>
        public ActionResult TwoContact()
        {
            return View();
        }
        /// <summary>
        /// 双联系发布
        /// </summary>
        /// <returns></returns>
        public ActionResult TwoContactPub()
        {
            return View();
        }
        /// <summary>
        /// 履职档案
        /// </summary>
        /// <returns></returns>
        public ActionResult Resumption()
        {
            return View();
        }
        /// <summary>
        /// 履职档案 代表
        /// </summary>
        /// <returns></returns>
        public ActionResult ResumptionUser()
        {
            return View();
        }
        public ActionResult ResumptionDetail(string Id)
        {
            CollectionEntity collectionEntity = new CollectionEntity();
            collectionEntity = collectionBll.GetEntity(Id);
            ViewBag.CollectionEntity = collectionEntity;
            return View(); 
        }
        /// <summary>
        /// 留言回音
        /// </summary>
        /// <returns></returns>
        public ActionResult MessageReply()
        {
            return View();
        }
        /// <summary>
        /// 留言回音 详情
        /// </summary>
        /// <returns></returns>
        public ActionResult MessageReplyDetail(string Id)
        {
            CollectionEntity collectionEntity = new CollectionEntity();
            collectionEntity = collectionBll.GetEntity(Id);
            ViewBag.CollectionEntity = collectionEntity;
            return View();
        }
        #endregion

        #region MyRegion 获取数据
        /// <summary>
        /// 获取评论
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="sidx"></param>
        /// <param name="sord"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public ActionResult GetCommentPageList(string pageIndex, string sidx, string sord, string queryJson)
        {
            Pagination pagination = new Pagination();
            pagination.page = string.IsNullOrEmpty(pageIndex) ? 1 : Convert.ToInt32(pageIndex);
            pagination.rows = 10;
            pagination.sidx = sidx;
            pagination.sord = sord;
            var data = commentBLL.GetPageList(pagination, queryJson).ToList();
            return Success(data.ToJson());
        }
        #endregion
        #region MyRegion 提交数据
        public ActionResult SaveForm(string keyValue, CommentEntity commentEntity)
        { 
            commentBLL.SaveForm(keyValue, commentEntity); 
            return Success("文明上网，审核后可以显示消息了");
        }
        #endregion







    }
     

}