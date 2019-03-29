using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc; 
using Senparc.Weixin.MP.AdvancedAPIs.OAuth;
using sys.Dal.Busines.BaseManage;
using sys.Dal.Entity;
using sys.Dal.Entity.BaseManage;
using sys.Util.WeChatSDK; 
namespace sys.Application.app.Controllers
{
    [ActionException]
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
        public UserInfoModel SessionUserInfo
        {
            get
            {
                if (Session["UserInfo"] == null)
                {
                    Session["UserInfo"] = new UserBLL().GetUserSearch(new UserEntity { UserId= SessionUserId });
                }
                return Session["UserInfo"] as UserInfoModel;
            }
            set { Session["UserInfo"] = value; }
        }
      
    }
}