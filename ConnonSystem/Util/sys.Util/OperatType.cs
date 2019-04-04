using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sys.Util
{
   public enum OperatType
    {
        /// <summary>
        /// 浏览量+1
        /// </summary>
        PV = 0,
        /// <summary>
        /// 已读+1
        /// </summary>
        IsRead = 1,

        /// <summary>
        /// 点赞
        /// </summary>
        IsLike = 2,

        /// <summary>
        /// app端已读
        /// </summary>
        AppRead = 3,

        /// <summary>
        /// PC端已读
        /// </summary>
        PCRead = 4
    }
}
