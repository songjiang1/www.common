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
    public class NotifyEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        ///  主键
        /// </summary>		
        public string NotifyId { get; set; }
        /// <summary>
        /// 用户
        /// </summary>		
        public string UserId { get; set; }
        /// <summary>
        /// 手机
        /// </summary>		
        public string Mobile { get; set; }
        
        /// <summary>
        /// 验证码
        /// </summary>		
        public string Code { get; set; }
        /// <summary>
        /// 发送状态 1：已发送，0未发送
        /// </summary>		
        public bool? Status { get; set; }
        /// <summary>
        /// 类型：如微信注册认证
        /// </summary>		
        public string Type { get; set; } 
        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime? ExpiresDate { get; set; }
        
        /// <summary>
        /// 添加日期
        /// </summary>		
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// 内容
        /// </summary>		
        public string Content { get; set; }
        #endregion

        #region 扩展操作

        #endregion
    }
}
