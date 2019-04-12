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

namespace sys.Application.app.Controllers
{
    public class TVCollectionController : BaseController
    {
        System.ComponentModel.NullableConverter nullableDateTime = new System.ComponentModel.NullableConverter(typeof(DateTime?));
        private NoticeBLL noticeBLL = new NoticeBLL();
        private MessageReadBLL messageReadBLL = new MessageReadBLL();
        private CollectionBLL collectionBll = new CollectionBLL();
        private MeetingBLL meetingBLL = new MeetingBLL();
        private SurveyBaseBLL surveyBaseBLL = new SurveyBaseBLL();
        private SurveyQuestionBLL surveyQuestionBLL = new SurveyQuestionBLL();
        private SurveyOptionsBLL surveyOptionsBLL = new SurveyOptionsBLL();

        private string meetingCategory = "1";//分类：会议是1，公告是2,3是问卷调查
        private string noticeCategory = "2";//分类：会议是1，公告是2,3是问卷调查
        private string surveyCategory = "3";//分类：会议是1，公告是2,3是问卷调查
        //[NeedOAuth]
        // GET: Home
        #region MyRegion 视图
        public ActionResult Index()
        { 
            return View();
        }
        #endregion

        #region MyRegion 获取数据
        /// <summary>
        /// 获取TV数据 按人名
        /// </summary>
        /// <param name="ParentId"></param>
        /// <param name="TVShowMenuId"></param>
        /// <param name="OrganizeId"></param>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        public ActionResult GeRspresentJsonList(string ParentId, string TVShowMenuId, string OrganizeId, string Title)
        {
            CollectionEntity collectionEntity = new CollectionEntity();
            collectionEntity.ParentId = ParentId;
            collectionEntity.TVShowMenuId = TVShowMenuId;
            collectionEntity.OrganizeId = OrganizeId;
            collectionEntity.Title = Title;
            var dtInitials = collectionBll.GetInitials(collectionEntity);
            var dtrows = collectionBll.GetTable(collectionEntity, "");
            var JsonData = new
            {
                dtInitials = dtInitials,
                dtrows = dtrows,
            };
            return Success(JsonData.ToJson());
        }


        /// <summary>
        /// 获取TV数据 按区域
        /// </summary>
        /// <param name="ParentId"></param>
        /// <param name="TVShowMenuId"></param>
        /// <param name="OrganizeId"></param>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        public ActionResult GeRspresentAddressList(string ParentId, string TVShowMenuId, string OrganizeId, string Title, string UserId = "", string OrderBy = "")
        {
            CollectionEntity collectionEntity = new CollectionEntity();
            collectionEntity.ParentId = ParentId;
            collectionEntity.TVShowMenuId = TVShowMenuId;
            collectionEntity.OrganizeId = OrganizeId;
            collectionEntity.Title = Title;
            collectionEntity.CreateUserId = UserId;
            var dtrows = collectionBll.GetTable(collectionEntity, OrderBy);
            return Success(dtrows.ToJson());
        }

        /// <summary>
        /// 获取TV数据 分页下拉数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="sidx">排序字段</param>
        /// <param name="sord">升降序 DESC,CREATEDate Desc</param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public ActionResult GeCollectionPageList(string pageIndex, string sidx, string sord, string queryJson)
        {
            Pagination pagination = new Pagination();
            pagination.page = string.IsNullOrEmpty(pageIndex) ? 1 : Convert.ToInt32(pageIndex);
            pagination.rows = 10;
            pagination.sidx = sidx;
            pagination.sord = sord;
            var data = collectionBll.GetPageList(pagination, queryJson).ToList();
            return Success(data.ToJson());
        }



        #endregion


        #region MyRegion 提交数据  
 
        #endregion


        

    }
     

}