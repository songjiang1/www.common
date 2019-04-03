using System;
using System.Web.Mvc; 
using Senparc.Weixin;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.OAuth;
using sys.Util.WeChatSDK;
using sys.Dal.Entity;

namespace sys.Application.app.Controllers.Extensions
{
    public class OAuth2Controller : BaseController
    {
        //下面换成账号对应的信息，也可以放入web.config等地方方便配置和更换
        private readonly string appId = WeixinConfig.Instance.AppId;
        private readonly string secret = WeixinConfig.Instance.AppSecret;

        public ActionResult Index()
        {
            //此页面引导用户点击授权
            return View();
        }

        /// <summary>
        /// OAuthScope.snsapi_userinfo方式回调
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public ActionResult UserInfoCallback(string code, string state)
        {
            //Logging.Log.Info("执行UserInfoCallback成功");
            if (string.IsNullOrEmpty(code))
            {
                return Content("您拒绝了授权！");
            }

            if (state !=sys.Util.Config.GetValue("WeiXinOauthState") )
            {
                //这里的state其实是会暴露给客户端的，验证能力很弱，这里只是演示一下
                //实际上可以存任何想传递的数据，比如用户ID，并且需要结合例如下面的Session["OAuthAccessToken"]进行验证
                return Content("验证失败！请从正规途径进入！");
            } 
            OAuthAccessTokenResult result = null;

            //通过，用code换取access_token
            try
            {
                result = OAuthApi.GetAccessToken(appId, secret, code);
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
            if (result.errcode != ReturnCode.请求成功)
            {
                return Content("错误：" + result.errmsg);
            }
            //下面2个数据也可以自己封装成一个类，储存在数据库中（建议结合缓存）
            //如果可以确保安全，可以将access_token存入用户的cookie中，每一个人的access_token是不一样的
            //Session["OAuthAccessTokenStartTime"] = DateTime.Now;
            //Session["OAuthAccessToken"] = result;
            //SessionUnionId = result.unionid;
            SessionOpenId = result.openid;
            //因为第一步选择的是OAuthScope.snsapi_userinfo，这里可以进一步获取用户详细信息
            try
            {
                OAuthUserInfo oauth = OAuthApi.GetUserInfo(result.access_token, result.openid); 
                return state == sys.Util.Config.GetValue("WeiXinOauthState") ? new RedirectResult(RedirectUrl) : new RedirectResult(sys.Util.Config.GetValue("RedirectUrl"));
            }
            catch (ErrorJsonResultException ex)
            {
                return Content(ex.Message);
            }
        }

        /// <summary>
        /// OAuthScope.snsapi_base方式回调
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public ActionResult BaseCallback(string code, string state)
        {
            if (string.IsNullOrEmpty(code))
            {
                return Content("您拒绝了授权！");
            }

            if (state != "CidTech")
            {
                //这里的state其实是会暴露给客户端的，验证能力很弱，这里只是演示一下
                //实际上可以存任何想传递的数据，比如用户ID，并且需要结合例如下面的Session["OAuthAccessToken"]进行验证
                return Content("验证失败！请从正规途径进入！");
            } 
            OAuthAccessTokenResult result = null;
            try
            {
                result = OAuthApi.GetAccessToken(appId, secret, code);
                
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
            if (result.errcode != ReturnCode.请求成功)
            {
                return Content("错误：" + result.errmsg);
            }
            //下面2个数据也可以自己封装成一个类，储存在数据库中（建议结合缓存）
            //如果可以确保安全，可以将access_token存入用户的cookie中，每一个人的access_token是不一样的
            //Session["OAuthAccessTokenStartTime"] = DateTime.Now;
            //Session["OAuthAccessToken"] = result;
            //SessionUnionId = result.unionid;
            SessionOpenId = result.openid;
            //因为这里还不确定用户是否关注本微信，所以只能试探性地获取一下
            try
            {
                //已关注，可以得到详细信息
                var uij = OAuthApi.GetUserInfo(result.access_token, result.openid);
                //WeChatService.Instance.WeixinSubscribe(new WeChatInfo()
                //{
                //    Country = uij.country,
                //    Province = uij.province,
                //    City = uij.city,
                //    HeadImgurl = uij.headimgurl,
                //    NickName = uij.nickname,
                //    Sex = uij.sex,
                //    OpenId = uij.openid,
                //    UnionId = uij.unionid
                //});
                //var wxUserInfo = OAuthApi.GetUserInfo(result.access_token, result.openid);
                //NickName = string.IsNullOrEmpty(userName) ? wxUserInfo.nickname : userName;
                //AccountPhoto = string.IsNullOrEmpty(userIcon) ? string.IsNullOrEmpty(string.Format("{0}", wxUserInfo.headimgurl)) ? "~/Content/Images/2.jpg" : wxUserInfo.headimgurl : userIcon;

                return state == sys.Util.Config.GetValue("WeiXinOauthState") ? new RedirectResult(RedirectUrl) : new RedirectResult(sys.Util.Config.GetValue("RedirectUrl"));

            }
            catch (ErrorJsonResultException ex)
            {
                //未关注，只能授权，无法得到详细信息
                //这里的 ex.JsonResult 可能为："{\"errcode\":40003,\"errmsg\":\"invalid openid\"}"
                return Content("用户已授权，授权Token：" + result);
            }
        }
    }
}