using sys.Aplication.Code;
using System;

namespace sys.Dal.Entity.AppManage
{
    /// <summary>
    /// 版 本 2.0
    /// Copyright (c)  
    /// 创建人：宋江
    /// 日 期：2015.12.7 16:40
    /// 描 述：新闻中心
    /// </summary>
    public class MeetingEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 新闻主键
        /// </summary>		
        public string MeetingId { get; set; }
        /// <summary>
        /// 完整标题
        /// </summary>		
        public string FullHead { get; set; }
        /// <summary>
        /// 标题颜色
        /// </summary>		
        public string FullHeadColor { get; set; }
        /// <summary>
        /// 简略标题
        /// </summary>		
        public string BriefHead { get; set; }

        /// <summary>
        /// 会议开始时间
        /// </summary>
        public DateTime? ConveneSTime { get; set; }
        /// <summary>
        /// 会议结束时间
        /// </summary>
        public DateTime? ConveneETime { get; set; }
        /// <summary>
        /// 会议地点
        /// </summary>		
        public string MeetingAddress { get; set; }
        /// <summary>
        /// 参会人员主键
        /// </summary>		
        public string ObjectId { get; set; }
        /// <summary>
        /// 参会人员姓名
        /// </summary>		
        public string ObjectName { get; set; }
        /// <summary>
        /// Tag 标签
        /// </summary>		
        public string TagWord { get; set; }
        /// <summary>
        /// 描述
        /// </summary>		
        public string Description { get; set; }
        
        /// <summary>
        /// 新闻内容
        /// </summary>		
        public string MeetingContent { get; set; }
        /// <summary>
        /// 浏览量
        /// </summary>		
        public int? PV { get; set; }
        
        /// <summary>
        /// 排序码
        /// </summary>		
        public int? SortCode { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>		
        public int? DeleteMark { get; set; }
        /// <summary>
        /// 有效标志
        /// </summary>		
        public int? EnabledMark { get; set; }
         
        /// <summary>
        /// 创建日期
        /// </summary>		
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// 创建用户主键
        /// </summary>		
        public string CreateUserId { get; set; }
        /// <summary>
        /// 创建用户
        /// </summary>		
        public string CreateUserName { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>		
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// 修改用户主键
        /// </summary>		
        public string ModifyUserId { get; set; }
        /// <summary>
        /// 修改用户
        /// </summary>		
        public string ModifyUserName { get; set; }
        /// <summary>
        /// 封面图
        /// </summary>
        public string Cover { get; set; }
        
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.MeetingId = Guid.NewGuid().ToString();
            this.CreateDate = DateTime.Now; 
            this.CreateUserId = OperatorProvider.Provider.Current().UserId;
            this.CreateUserName = OperatorProvider.Provider.Current().UserName;
            this.DeleteMark = 0;
            this.EnabledMark = 1;
            this.PV = 0;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.MeetingId = keyValue;
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = OperatorProvider.Provider.Current().UserId;
            this.ModifyUserName = OperatorProvider.Provider.Current().UserName;
        }
        #endregion
    }
}
