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
        PCRead = 4,
        /// <summary>
        /// 签到 
        /// </summary>
        SignIn = 5,
        /// <summary>
        /// 确认 参加
        /// </summary>
        AttendExpo = 6,

        /// <summary>
        /// 确认 不参加
        /// </summary>
        NoAttendExpo = 7,

        /// <summary>
        /// 执行提交操作
        /// </summary>
        Submit=8,

        /// <summary>
        /// 打分
        /// </summary>
        Score = 9,
        /// <summary>
        /// 参与人数
        /// </summary>
        JoinCount = 10,

    }
}
