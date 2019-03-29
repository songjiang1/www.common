using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace sys.Util
{
    public static class WebUtility
    {
        /// <summary>
        /// 获取ip地址
        /// </summary>
        public static string IP
        {
            get
            {
                return HttpContext.Current.Request.UserHostAddress;
            }
        }

        public static bool IsMobile(string mobileNum)
        {
            Regex r = new Regex("^(1\\d{10})$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            return r.IsMatch(mobileNum);

        }

        public static bool IsEmail(string email)
        {
            Regex r = new Regex("^([a-zA-Z0-9_\\.\\-])+\\@(([a-zA-Z0-9\\-])+\\.)+([a-zA-Z0-9]{2,4})+$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            return r.IsMatch(email);

        }

        public static string MD5(string str)
        {
            byte[] hashvalue = (new MD5CryptoServiceProvider()).ComputeHash(UTF8Encoding.UTF8.GetBytes(str));
            return BitConverter.ToString(hashvalue).Replace("-", "").ToLower();

        }

    }
}
