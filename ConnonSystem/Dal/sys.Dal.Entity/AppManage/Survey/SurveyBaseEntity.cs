using sys.Aplication.Code;
using System;

namespace sys.Dal.Entity.AppManage
{
    /// <summary>
    /// 版 本 1.0
    /// Copyright (c)  
    /// 创建人：宋江
    /// 日 期：2015.12.7 16:40
    /// 描 述：问卷调查 基本信息
    /// </summary>
    public class SurveyBaseEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 主键
        /// </summary>		
        public string  Id { get; set; }
        /// <summary>
        /// 标题
        /// </summary>		
        public string Title { get; set; }
        /// <summary>
        /// 标题颜色
        /// </summary>		
        public string TitleColor { get; set; }
       

        /// <summary>
        /// 问卷开始时间
        /// </summary>
        public DateTime? OperateSDate { get; set; }
        /// <summary>
        /// 问卷结束时间
        /// </summary>
        public DateTime? OperateEDate { get; set; }
       
        /// <summary>
        /// 参与人员姓名
        /// </summary>		
        public string ObjectName { get; set; }
       
        /// <summary>
        /// 描述
        /// </summary>		
        public string Description { get; set; }
        
      
        /// <summary>
        /// 参与人员数量
        /// </summary>		
        public int? JoinCount { get; set; }
        /// <summary>
        /// 浏览量数量
        /// </summary>		
        public int? PV { get; set; }
        /// <summary>
        /// 标志 是否截止
        /// </summary>
        public int? Flag { get; set; }
        /// <summary>
        /// 只是用于查询返回状态标识；0未读，1已读未完成，2已读已完成
        /// </summary>
        public int? State { get; set; }
        /// <summary>
        /// 排序码
        /// </summary>		
        public int? SortCode { get; set; } 
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
        /// 分类
        /// </summary>
        public string Category { get; set; }

        
        
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreateDate = DateTime.Now; 
            this.CreateUserId = OperatorProvider.Provider.Current().UserId;
            this.CreateUserName = OperatorProvider.Provider.Current().UserName; 
            this.EnabledMark = 1;
            this.JoinCount = 0;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.Id = keyValue;
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = OperatorProvider.Provider.Current().UserId;
            this.ModifyUserName = OperatorProvider.Provider.Current().UserName;
        }
        #endregion
    }
}
