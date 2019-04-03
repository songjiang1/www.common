using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace AppQuartz
{
    /// <summary>
    /// 消息推送通知
    /// </summary>
    public class SendWeChatMessage
    {
          
       /// <summary>
       /// 发送模板消息
       /// </summary>
       /// <param name="data"></param>
       /// <param name="access_token"></param>
       /// <returns></returns>
       public static bool ModelMessageSend(string data, string access_token)
       {
           string url = "https://api.weixin.qq.com/cgi-bin/message/template/send?access_token=" + access_token;
           string result = HttpPost(url, data);
            JavaScriptSerializer Jss = new JavaScriptSerializer();
           Dictionary<string, object> respDic = (Dictionary<string, object>)Jss.DeserializeObject(string.Format("{0}", result));
           //if (result.Contains("ok"))  判断是否推送成功
           if (respDic["errcode"].ToString() == "0" && respDic["errmsg"].ToString() == "ok")
           {
               return true;
           }
           else
           {
               return false;
           }
       }

   
       /// <summary>
       /// 生成对应的推送微信 模板消息
       /// </summary>
       /// <param name="type">消息类型</param> 
       /// <param name="UserOpenId">用户openID</param>
       /// <param name="template_id">模板id</param>
       /// <param name="url">详情连接</param>
       /// <param name="keyword1">主题</param>
       /// <param name="keyword2">提交人</param>
       /// <param name="keyword3">内容</param>
       /// <param name="keyword4">消息时间</param>
       /// <param name="keyword5">消息类型</param>
       /// <param name="keyword6">开始时间</param>
       /// <param name="keyword7">结束时间</param>
       /// <param name="keyword8">地点</param>
       /// <param name="remark">备注</param>
       /// <param name="color">颜色</param>
       /// <param index=14 name="keyword9">票数</param>
       /// <param index=15 name="keyword10">排名</param>
       /// <returns></returns>
       public static string GetTemplateMessage(string type, string UserOpenId, string template_id, string url = "", string keyword1 = "", string keyword2 = "", string keyword3 = "", string keyword4 = "", string keyword5 = "", string keyword6 = "", string keyword7 = "", string keyword8 = "", string remark = "", string color = "#173177", string keyword9 = "", string keyword10 = "")
       {
           string TemplateMssString = "";
           switch (type)
           {
               case "123":
                   //TemplateMssString = "{\"touser\":\"" + UserOpenId + "\",\"template_id\":\"" + template_id + "\",\"url\":\"" + url + "\",\"data\":{\"first\": {\"value\":\"" + typename + "\",\"color\":\"" + color + "\"},\"keyword1\":{\"value\":\"" + keyword1 + "\",\"color\":\"" + color + "\"}, \"keyword2\":{\"value\":\"" + keyword2 + "\",\"color\":\"" + color + "\"},\"keyword3\":{\"value\":\"" + keyword3 + "\",\"color\":\"" + color + "\"},\"keyword4\":{\"value\":\"" + keyword4 + "\",\"color\":\"" + color + "\"},\"keyword5\":{\"value\":\"" + keyword5 + "\",\"color\":\"" + color + "\"},\"remark\":{\"value\":\"" + remark + "\",\"color\":\"" + color + "\"}}}";
                   remark = string.IsNullOrWhiteSpace(remark) ? "请您尽快处理!" : remark;
                   TemplateMssString = "{\"touser\":\"" + UserOpenId + "\",\"template_id\":\"" + template_id + "\",\"url\":\"" + url + "\",\"data\":{\"first\": {\"value\":\"" + keyword3 + "\",\"color\":\"" + color + "\"},\"keyword1\":{\"value\":\"" + keyword1 + "\",\"color\":\"" + color + "\"}, \"keyword2\":{\"value\":\"" + keyword4 + "\",\"color\":\"" + color + "\"},\"remark\":{\"value\":\"" + remark + "\",\"color\":\"" + color + "\"}}}";
                   break;
               case "100201": //会议通知
                   remark = string.IsNullOrWhiteSpace(remark) ? "感谢您的参与，祝参会愉快！" : remark; 
                   TemplateMssString = "{\"touser\":\"" + UserOpenId + "\",\"template_id\":\"" + template_id + "\",\"url\":\"" + url + "\",\"data\":{\"first\": {\"value\":\"" + keyword3 + "\",\"color\":\"" + color + "\"},\"keyword1\":{\"value\":\"" + keyword1 + "\",\"color\":\"" + color + "\"}, \"keyword2\":{\"value\":\"" + keyword2 + "\",\"color\":\"" + color + "\"}, \"keyword3\":{\"value\":\"" + keyword6 + "\",\"color\":\"" + color + "\"}, \"keyword4\":{\"value\":\"" + keyword7 + "\",\"color\":\"" + color + "\"}, \"keyword5\":{\"value\":\"" + keyword8 + "\",\"color\":\"" + color + "\"},\"remark\":{\"value\":\"" + remark + "\",\"color\":\"" + color + "\"}}}";
                   break;
               case "900001"://投票成功通知
                   remark = string.IsNullOrWhiteSpace(remark) ? "您的投票至关重要,恭喜您投票成功！" : remark;
                   TemplateMssString = "{\"touser\":\"" + UserOpenId + "\",\"template_id\":\"" + template_id + "\",\"url\":\"" + url + "\",\"data\":{\"first\": {\"value\":\"" + keyword3 + "\",\"color\":\"" + color + "\"},\"keyword1\":{\"value\":\"" + keyword1 + "\",\"color\":\"" + color + "\"}, \"keyword2\":{\"value\":\"" + keyword4 + "\",\"color\":\"" + color + "\"},\"remark\":{\"value\":\"" + remark + "\",\"color\":\"" + color + "\"}}}";
                   break;
               case "900002"://投票结果通知 
                   TemplateMssString = "{\"touser\":\"" + UserOpenId + "\",\"template_id\":\"" + template_id + "\",\"url\":\"" + url + "\",\"data\":{\"first\": {\"value\":\"" + keyword3 + "\",\"color\":\"" + color + "\"},\"keyword1\":{\"value\":\"" + keyword2 + "\",\"color\":\"" + color + "\"}, \"keyword2\":{\"value\":\"" + keyword9 + "\",\"color\":\"" + color + "\"}, \"keyword3\":{\"value\":\"" + keyword10 + "\",\"color\":\"" + color + "\"},\"remark\":{\"value\":\"" + remark + "\",\"color\":\"" + color + "\"}}}";
                   break;
               default:
                   TemplateMssString = "";
                   break;
           }
           return TemplateMssString;
       }
        /// <summary>
       /// 从数据库中获取模板消息 生成对应的推送微信 
        /// </summary>
       /// <param name="TemplateMessageCode">模板消息,json串</param>
       /// <param name="UserOpenId">用户openID</param>
       /// <param name="template_id">模板id</param>
       /// <param name="url">详情连接</param>
       /// <param name="keyword1">主题</param>
       /// <param name="keyword2">提交人</param>
       /// <param name="keyword3">内容</param>
       /// <param name="keyword4">消息时间</param>
       /// <param name="keyword5">消息类型</param>
       /// <param name="keyword6">开始时间</param>
       /// <param name="keyword7">结束时间</param>
       /// <param name="keyword8">地点</param>
       /// <param name="remark">备注</param>
       /// <param name="color">颜色</param>
       /// <param index=14 name="keyword9">票数</param>
       /// <param index=15 name="keyword10">排名</param>
       /// <returns></returns>
       public static string GetDataBaseTemplate(string TemplateMessageCode, string UserOpenId, string template_id, string url = "", string keyword1 = "", string keyword2 = "", string keyword3 = "", string keyword4 = "", string keyword5 = "", string keyword6 = "", string keyword7 = "", string keyword8 = "", string remark = "", string color = "#173177", string keyword9 = "", string keyword10 = "")
       {
           string TemplateMssString="";
           TemplateMssString = string.Format(TemplateMessageCode,"", UserOpenId, template_id, url, keyword1, keyword2, keyword3, keyword4, keyword5, keyword6, keyword7, keyword8, remark, color, keyword9, keyword10);
           return TemplateMssString;
       }
       /// <summary>
       /// post请求
       /// </summary>
       /// <param name="url"></param>
       /// <param name="postData"></param>
       /// <returns></returns>
       public static string HttpPost(string url, string postData)
       {
           byte[] data = Encoding.UTF8.GetBytes(postData);
           HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
           request.Method = "Post";
           request.ContentType = "application/json";
           request.ContentLength = data.Length;
           request.KeepAlive = true;

           Stream stream = request.GetRequestStream();
           stream.Write(data, 0, data.Length);

           HttpWebResponse response;
           try
           {
               response = (HttpWebResponse)request.GetResponse();
           }
           catch (WebException ex)
           {
               response = (HttpWebResponse)ex.Response;
           }

           StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
           string content = reader.ReadToEnd();

           request.Abort();
           response.Close();
           reader.Dispose();
           stream.Close();
           stream.Dispose();

           return content;
       }

      
    }
}
