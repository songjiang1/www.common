using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sys.Dal.Entity.AppManager
{
    /// <summary>
    /// 版 本 2.0
    /// Copyright (c)  
    /// 创建人：宋江
    /// 日 期：2015.12.8 11:31
    /// 描 述：邮件分类
    /// </summary>
    public class MessageReadEntity: BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 邮件分类主键
        /// </summary>		
        public string Uniqueid { get; set; }
        /// <summary>
        /// 分类名称
        /// </summary>		
        public string MessageId { get; set; }
        /// <summary>
        /// 排序码
        /// </summary>		
        public string UserId { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>		
        public bool? Flag { get; set; }
        /// <summary>
        /// 有效标志
        /// </summary>		
        public bool? IsRead { get; set; }
        /// <summary>
        /// 备注
        /// </summary>		
        public bool? IsLike { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>		
        public string Category { get; set; }
        /// <summary>
        /// 创建用户主键
        /// </summary>		
        public string AppRead { get; set; }
        /// <summary>
        /// 创建用户
        /// </summary>		
        public string PCRead { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>		
        public DateTime? CreateDate { get; set; }
        #endregion

        #region 扩展操作

        #endregion
    }
}
