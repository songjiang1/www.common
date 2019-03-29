using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sys.Util
{
    public static class CCPSMSNotifyUtil
    { 
        #region 短信通知

        /// <summary>
        /// 测试短信验证
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="code"></param>
        /// <param name="expires"></param>
        public static ReturnResult  TestNotify(string mobile,string code,string expires)
        {
           return  CCPSMSHelper.SendTemplateSMS(mobile, "1", new string[] { code,expires });
        }
        /// <summary>
        /// 注册验证
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="code"></param>
        /// <param name="expires"></param>
        public static void RegisterNotify(string mobile, string code, string expires)
        {
            CCPSMSHelper.SendTemplateSMS(mobile, "227972", new string[] { code, expires });
        }
        /// <summary>
        /// 系统通知
        /// </summary>
        /// <param name="mobile"></param> 
        public static void Notice(string mobile, string code, string expires)
        {
            CCPSMSHelper.SendTemplateSMS(mobile, "227972", new string[] { code, expires });
        }
        #endregion   




    }
}
