using System;
using System.Collections.Generic;
using System.Linq;
using System.Web; 
namespace sys.Util
{
    /// <summary>
    /// 容联辅助类
    /// </summary>
    public class CCPSMSHelper
    {
        private static string accountSid;
        private static string token;
        private static string appId;
        private static string apptoken;
        private static string url;
        private static string port;

        //static CCPSMSHelper()
        //{
        //    accountSid = "8aaf0708697b6beb0169c32e724d3225";
        //    token = "46f51811a8964de28aeb5044bbb26b70";
        //    appId = "8aaf0708697b6beb0169c32e729a322b";
        //    apptoken = "7ca703d53dc832fd6b3c50ea95576690"; 
        //    url = "app.cloopen.com";
        //    port = "8883";
        //}
        static CCPSMSHelper()
        {
            accountSid = Config.GetValue("CCPaccountSid");
            token = Config.GetValue("CCPtoken");
            appId = Config.GetValue("CCPappId");
            apptoken = Config.GetValue("CCPapptoken");
            url = Config.GetValue("CCPurl");
            port = Config.GetValue("CCPport");
        }
        // return SMSService.SendTemplateSMS(mobile, "227982", new string[] { code,"30" }, "vender");
        public static int SendTemplateSMS(string mobile, string template, string[] data)
        {
            if (!ValidateUtil.IsMobile(mobile))
            {
                return 1;
            }
            try
            {
                //初始化短信接口
                 CCPRestSDK api = new CCPRestSDK();
                //ip格式如下，不带https://
                bool isInit = api.init(url, port);
                api.setAccount(accountSid, token);
                api.setAppId(appId);
                api.enabeLog(true); 
                Dictionary<string, object> retData = new Dictionary<string, object>();
                if (isInit)
                {
                    //短信发送
                    retData = api.SendTemplateSMS(mobile, template, data); 
                    var  ret = getDictionaryData(retData);
                    if (retData["statusCode"].ToString() == "000000")
                        return 0;
                    else
                    { 
                        return 1;
                    }
                }
                else
                {
                    return 1;
                }
            }
            catch { return 1; }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static string getDictionaryData(Dictionary<string, object> data)
        {
            string ret = null;
            foreach (KeyValuePair<string, object> item in data)
            {
                if (item.Value != null && item.Value.GetType() == typeof(Dictionary<string, object>))
                {
                    ret += item.Key.ToString() + "={";
                    ret += getDictionaryData((Dictionary<string, object>)item.Value);
                    ret += "};";
                }
                else
                {
                    ret += item.Key.ToString() + "=" + (item.Value == null ? "null" : item.Value.ToString()) + ";";
                }
            }
            return ret;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ReturnResult SendVoice(string mobile, string data)
        {
            if (!ValidateUtil.IsMobile(mobile))
            {
                return new ReturnResult { Msg = "错误的手机号", Code = 1 };
            }
            try
            {
                CCPRestSDK api = new CCPRestSDK();
                //ip格式如下，不带https://
                bool isInit = api.init(url, port);
                api.setAccount(accountSid, token);
                api.setAppId(appId);
                api.enabeLog(true);

                if (isInit)
                {
                    Dictionary<string, object> retData = api.VoiceVerify(mobile, data, "", "3", "");
                    if (retData["statusCode"].ToString() == "000000")
                    {
                        return new ReturnResult { Msg = "发送成功", Code = 0 };
                    }
                    else
                    {
                        return new ReturnResult { Msg = "发送失败", Code = 1 };
                    }
                }
                else
                {
                    return new ReturnResult { Msg = "短信模块初始化失败", Code = 1 };
                }
            }
            catch { return new ReturnResult { Msg = "发送失败", Code = 1 }; }
        }

        /// <summary>
        /// 创建子账户
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public ReturnResult CreateSubAccount(int userid)
        {
            CCPRestSDK api = new CCPRestSDK();
            bool isInit = api.init(url, port);
            if (isInit)
            {
                api.setAccount(accountSid, token);
                api.setAppId(appId);
                api.enabeLog(true);
                Dictionary<string, object> data = api.CreateSubAccount(userid.ToString());
                if (data["statusCode"].ToString() == "000000")
                {
                    Dictionary<string, object> retdata = ((data["data"] as Dictionary<string, object>)["SubAccount"] as Dictionary<string, object>);
                    return new ReturnResult { Msg = "", Code = 0, Obj = new { sid = retdata["subAccountSid"], account = retdata["voipAccount"], pwd = retdata["voipPwd"], token = retdata["subToken"] } };
                }
                else
                {
                    return new ReturnResult { Msg = "发送失败", Code = 1 };
                }
            }
            else
            {
                return new ReturnResult { Msg = "CCP初始化失败", Code = 1 };
            }
        }
    }
}