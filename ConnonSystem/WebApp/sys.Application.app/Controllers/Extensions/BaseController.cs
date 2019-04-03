using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc; 
using Senparc.Weixin.MP.AdvancedAPIs.OAuth;
using sys.Aplication.Code;
using sys.Dal.Busines.BaseManage;
using sys.Dal.Entity;
using sys.Dal.Entity.BaseManage;
using sys.Util;
using sys.Util.Log;
using sys.Util.WebControl;
using sys.Util.WeChatSDK; 
namespace sys.Application.app.Controllers.Extensions
{ 
    //[HandlerLogin(LoginMode.Ignore)]
    [HandlerLogin(LoginMode.Enforce)]
    public class BaseController : Controller
    {
        // GET: Base
        public BaseController()
        {
        }
        public string RedirectUrl
        {
            //暂时先保存到session 以后修改放入Redis缓存中
            get
            {
                if (Session["RedirectUrl"] != null)
                    return Session["RedirectUrl"].ToString();
                return string.Empty;
            }
            set { Session["RedirectUrl"] = value; }
        }

        public string SessionOpenId
        {
            //暂时先保存到session 以后修改放入Redis缓存中
            get
            {
                if (Session["OpenId"] != null)
                    return  Session["OpenId"].ToString();
                return string.Empty;
            }
            set { Session["OpenId"] = value; }
        }
        public string SessionUserId
        {
            //暂时先保存到session 以后修改放入Redis缓存中
            get
            {
                if (Session["UserId"] != null)
                    return Session["UserId"].ToString();
                return string.Empty;
            }
            set { Session["UserId"] = value; }
        }
        //public UserInfoModel SessionUserInfo
        //{
        //    get
        //    {
        //        if (Session["UserInfo"] == null)
        //        {
        //            Session["UserInfo"] = new UserBLL().GetUserSearch(new UserEntity { UserId= SessionUserId });
        //        }
        //        return Session["UserInfo"] as UserInfoModel;
        //    }
        //    set { Session["UserInfo"] = value; }
        //}


        #region MyRegion 返回信息配置
        private Log _logger;
        /// <summary>
        /// 日志操作
        /// </summary>
        public Log Logger
        {
            get { return _logger ?? (_logger = LogFactory.GetLogger(this.GetType().ToString())); }
        }
        /// <summary>
        /// 返回成功消息
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns></returns>
        protected virtual ActionResult ToJsonResult(object data)
        {
            return Content(data.ToJson());
        }
        /// <summary>
        /// 返回成功消息
        /// </summary>
        /// <param name="message">消息</param>
        /// <returns></returns>
        protected virtual ActionResult Success(string message)
        {
            return Content(new AjaxResult { type = ResultType.success, message = message }.ToJson());
        }
        /// <summary>
        /// 返回成功消息
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="data">数据</param>
        /// <returns></returns>
        protected virtual ActionResult Success(string message, object data)
        {
            return Content(new AjaxResult { type = ResultType.success, message = message, resultdata = data }.ToJson());
        }
        /// <summary>
        /// 返回失败消息
        /// </summary>
        /// <param name="message">消息</param>
        /// <returns></returns>
        protected virtual ActionResult Error(string message)
        {
            return Content(new AjaxResult { type = ResultType.error, message = message }.ToJson());
        }
        #endregion
    }
}