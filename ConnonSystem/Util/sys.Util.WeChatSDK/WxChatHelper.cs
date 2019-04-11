using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization; 
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;
using System.Data;
using RestSharp;
using Weixin.Mp.Sdk;
using Weixin.Mp.Sdk.Request;
using Weixin.Mp.Sdk.Domain;
using Weixin.Mp.Sdk.Response;
using sys.Dal.Busines;
using sys.Dal.Entity;

namespace sys.Util.WeChatSDK
{
    public class WxChatHelper
    {
      private  AccessTokenBLL accessTokenBLL = new AccessTokenBLL();
        /// <summary>
        /// 获取用户微信头像
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        public string GetUserWechatPhoto(string openid)
        {
            var headimgurl = string.Empty;
            if (!string.IsNullOrEmpty(openid))
            {
                string ACCESS_TOKEN = GetAccessToken();
                var client = new RestClient("https://api.weixin.qq.com/cgi-bin/user/info?access_token=" + ACCESS_TOKEN + "&openid=" + openid + "&lang=zh_CN");
                var request = new RestRequest(Method.GET);
                RestResponse response = client.Execute(request) as RestResponse;
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                Dictionary<string, object> jsonstr = (Dictionary<string, object>)serializer.DeserializeObject(response.Content);
                object value;
                if (jsonstr.TryGetValue("headimgurl", out value)) headimgurl = value.ToString();
            }
            return headimgurl;
        }

        //获取未关注用户的头像
        public string GetWxPhoto(string token, string openid)
        {
            var photoimgurl = string.Empty;
            if (!string.IsNullOrEmpty(openid) && !string.IsNullOrEmpty(token))
            {
                var client = new RestClient("https://api.weixin.qq.com/sns/userinfo?access_token=" + token + "&openid=" + openid + "&lang=zh_CN");
                var request = new RestRequest(Method.GET);
                RestResponse response = client.Execute(request) as RestResponse;
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                Dictionary<string, object> jsonstr = (Dictionary<string, object>)serializer.DeserializeObject(response.Content);
                object value;
                if (jsonstr.TryGetValue("headimgurl", out value)) photoimgurl = value.ToString();
            }
            return photoimgurl;
        }



        /// <summary>
        /// 获取微信接口凭证AccessToken
        /// </summary>
        /// <returns></returns>
        public string GetAccessToken(string keyValue="")
        {
            string accessToken = string.Empty;
            string _appId = WeixinConfig.Instance.AppId;
            string appSecret = WeixinConfig.Instance.AppSecret;
            IMpClient mpClient = new MpClient();
            AccessTokenGetRequest request = new AccessTokenGetRequest()
            {
                AppIdInfo = new AppIdInfo() { AppID = _appId, AppSecret = appSecret }
            };
            AccessTokenGetResponse response = mpClient.Execute(request);
            if (response.IsError)
            {
                return "0";
            }
            accessToken = response.AccessToken.AccessToken;
            int timestampNow = ConvertDateTimeInt(DateTime.Now);
            accessTokenBLL.SaveForm(keyValue,new AccessTokenEntity {
                Id = keyValue,
                ModifyDate =DateTime.Now,
                Timestamp= timestampNow,
                Token= accessToken
            });
            return accessToken;
        }

        /// <summary>
        /// 将DataTime转换为Int值
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public int ConvertDateTimeInt(DateTime time)
        {
            int intResult = 0;
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            intResult = Convert.ToInt32((time - startTime).TotalSeconds);
            return intResult;
        }
        public static string CreateTimestamp()
         {
             TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
             string timestamp = Convert.ToInt64(ts.TotalSeconds).ToString();
             return timestamp;
         }

    /// <summary>
    /// 从数据库中获取Access_token
    /// </summary>
    /// <returns></returns>
    public   string GetAccessToken_formDb()
        {
            string accessToken = string.Empty; 
            DataTable dt = accessTokenBLL.GetTable();
            if (dt.Rows.Count > 0)
            {
                DateTime date = DateTime.Now;
                var timestampNow = ConvertDateTimeInt(DateTime.Now);
                var timestamp = Convert.ToInt32(dt.Rows[0]["Timestamp"]);
                string keyValue= dt.Rows[0]["Id"].ToString();
                accessToken = string.Format("{0}", dt.Rows[0]["Token"]);
                //token失效
                if (timestampNow - timestamp >= 3000)
                {
                    accessToken = GetAccessToken(keyValue);
                    accessTokenBLL.SaveForm(keyValue, new AccessTokenEntity
                    {
                        Id = keyValue,
                        ModifyDate = DateTime.Now,
                        Timestamp = timestampNow,
                        Token = accessToken
                    });
                    

                }
            }
            return accessToken;
        }

        /// <summary>
        /// 获取微信 JsApiTicket
        /// 参考文档（见附录1）：http://mp.weixin.qq.com/wiki/11/74ad127cc054f6b80759c40f77ec03db.html
        /// </summary>
        /// <returns></returns>
        public string GetJsApiTicket()
        {
            int timestampNow = ConvertDateTimeInt(DateTime.Now);
            string token = GetAccessToken_formDb();
            string url = url = "https://api.weixin.qq.com/cgi-bin/ticket/getticket?type=jsapi&access_token=" + token + "";
            JObject callBack = (JObject)JsonConvert.DeserializeObject(HttpGet(url));
            if (callBack["errcode"].ToString() != "0") //报错，重现获取
            {
                string newactokenstr = string.Format("{0}", GetAccessToken());
                string newurl = "https://api.weixin.qq.com/cgi-bin/ticket/getticket?type=jsapi&access_token=" + newactokenstr + "";
                callBack = (JObject)JsonConvert.DeserializeObject(HttpGet(newurl).ToString());
            }
            string jsApiTicket = callBack["ticket"].ToString() + "," + timestampNow;
            return jsApiTicket;
        }
        private static readonly string tokenUrl = "https://api.weixin.qq.com/cgi-bin/token";

        private static readonly string ticket = "https://api.weixin.qq.com/cgi-bin/ticket/getticket";

     
        
        /// <summary>
        /// Http Get请求
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private string HttpGet(string url)
        {
            try
            {
                WebClient webClient = new WebClient();
                webClient.Credentials = CredentialCache.DefaultCredentials;
                Byte[] data = webClient.DownloadData(url);
                string responseMsg = Encoding.Default.GetString(data);
                return responseMsg;
            }
            catch (WebException ex)
            {
                //Log.Error("WebClient error:" ,ex.Message);
                return null;
            }
        }

        /// <summary>
        /// sha1加密
        /// </summary>
        /// <param name="str_sha1"></param>
        /// <returns></returns>
        public string SHA1_Hash(string str_sha1)
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            byte[] bytes_sha1_in = System.Text.UTF8Encoding.Default.GetBytes(str_sha1);
            byte[] bytes_sha1_out = sha1.ComputeHash(bytes_sha1_in);
            string str_sha1_out = BitConverter.ToString(bytes_sha1_out);
            str_sha1_out = str_sha1_out.Replace("-", "").ToLower();
            return str_sha1_out;
        }

        /// <summary>
        ///     获取微信用户的信息
        /// </summary>
        /// <param name="accessTocken"></param>
        /// <param name="openid"></param>
        /// <returns></returns>
        public Dictionary<string, object> GetUserInfo_WeiXin(string accessTocken, string openid)
        {
            var client =
                new RestClient("https://api.weixin.qq.com/cgi-bin/user/info?access_token=" + accessTocken + "&openid=" +
                               openid + "&lang=zh_CN");
            var request = new RestRequest(Method.GET);
            var response = client.Execute(request) as RestResponse;
            if (response != null)
            {
                var responseMsg = response.Content;
                var serializer = new JavaScriptSerializer();
                var json = (Dictionary<string, object>)serializer.DeserializeObject(responseMsg);
                return json;
            }
            return null;
        }

    }
}