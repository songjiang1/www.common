using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sys.Dal.Entity
{
    /// <summary>
    /// 版 本 2.0
    /// Copyright (c)  
    /// 创建人：宋江
    /// 日 期：2015.12.8 11:31
    /// 描 述：邮件分类
    /// </summary>
    public class AccessTokenEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        ///  主键
        /// </summary>		
        public string Id { get; set; }

        /// <summary>
        /// Token
        /// </summary>		
        public string Token { get; set; } 
        /// <summary>
        /// 时间戳
        /// </summary>		
        public long Timestamp { get; set; } 
        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime? ModifyDate { get; set; }
      
        #endregion

        #region 扩展操作

        #endregion
    }
}
