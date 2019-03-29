using System.Web.Mvc;
using Senparc.Weixin.BrowserUtility;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.AdvancedAPIs;
using sys.Util.WeChatSDK;

namespace sys.Application.app.Controllers
{
    public class NeedOAuthAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var bc = filterContext.Controller as BaseController;
            if (bc == null) return;
            //正式环境执行

            if (BroswerUtility.SideInWeixinBroswer(filterContext.HttpContext))
            {
                if (!string.IsNullOrEmpty(bc.SessionOpenId))
                {
                    base.OnActionExecuting(filterContext);
                }
                else
                {
                    bc.RedirectUrl = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
                    var oauthurl = OAuthApi.GetAuthorizeUrl(WeixinConfig.Instance.AppId, WeixinConfig.Instance.WeixinServer + "/oauth2/UserInfoCallback", sys.Util.Config.GetValue("WeiXinOauthState"), OAuthScope.snsapi_userinfo);
                    filterContext.Result = new RedirectResult(oauthurl);
                }
            }
            else //微信外
            {
                filterContext.Result = new RedirectResult("/Account/Login");
            }

            base.OnActionExecuting(filterContext);
        }
    }
}