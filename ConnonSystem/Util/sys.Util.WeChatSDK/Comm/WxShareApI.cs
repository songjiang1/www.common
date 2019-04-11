using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Security;


namespace sys.Util.WeChatSDK
{
    public class WxShareApI
    {
        /// <summary>
        /// 保存页面对象，因为要在类的方法中使用Page的Request对象
        /// </summary>
        private Page page { get; set; }
        //微信分享配置项
        public string appId = WeixinConfig.Instance.AppId;
        public string timestamp = GenerateTimeStamp();
        public string nonceStr = GenerateNonceStr();

        public string JsApiTicket
        {
            get; set;
        }
        //public string signature { get; set; }

        ////微信分享内容
        ///// <summary>
        ///// 分享标题
        ///// </summary>
        //public string Title { get; set; }
        ///// <summary>
        ///// 分享链接
        ///// </summary>
        //public string Link {get; set; }
        ///// <summary>
        ///// 分享描述
        ///// </summary>
        //public string Desc { get; set;}
        ///// <summary>
        ///// 分享图片
        ///// </summary>
        //public string ImgUrl { get; set;}
        //public WxShareApI(Page page)
        //{
        //    this.page = page;
        //}

        /**
       * 生成时间戳，标准北京时间，时区为东八区，自1970年1月1日 0点0分0秒以来的秒数
        * @return 时间戳
       */
        public static string GenerateTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        /**
        * 生成随机串，随机串包含字母或数字
        * @return 随机串
        */
        public static string GenerateNonceStr()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }

        /// <summary>
        /// 获取用于JsApi分享功能的签名signature
        /// </summary>
        /// <returns></returns>
        public string GetSignature(string url)
        {
            WxChatHelper wxChartInfo = new WxChatHelper();
            //string url = page.Request.Url.AbsoluteUri.ToString().Trim().Split('#')[0]; //Replace("http://", " ")
            if (string.IsNullOrEmpty(JsApiTicket))
            {
                JsApiTicket = wxChartInfo.GetJsApiTicket();
            }
            else
            {
                int timestampOld = Convert.ToInt32(JsApiTicket.Split(',')[1]);
                int timestampNow = wxChartInfo.ConvertDateTimeInt(DateTime.Now);
                if (timestampNow - timestampOld >= 3000)
                {
                    JsApiTicket = wxChartInfo.GetJsApiTicket();
                }
            }
            //string jsApiTicket = wxChartInfo.GetJsApiTicket();
            string rawstring = "jsapi_ticket=" + JsApiTicket.Split(',')[0] + "&noncestr=" + nonceStr + "&timestamp=" + timestamp + "&url=" + url + "";
            return wxChartInfo.SHA1_Hash(rawstring);
        }
        public string GetSignature(string timestamp, string noncestr, string url)
        {
            WxChatHelper wxChartInfo = new WxChatHelper();
            string string1 = "jsapi_ticket=" +new WxChatHelper().GetAccessToken_formDb() + "&noncestr=" + noncestr + "&timestamp=" + timestamp + "&url=" + url;
            //使用sha1加密这个字符串
            return wxChartInfo.SHA1_Hash(string1);
        }

    }
}