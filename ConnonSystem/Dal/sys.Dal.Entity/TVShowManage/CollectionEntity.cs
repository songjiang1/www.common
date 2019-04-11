using System;
using sys.Aplication.Code;
using sys.Dal.Entity;

namespace sys.Dal.Entity.TVShowManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c)  
    /// 创建人：宋江
    /// 日 期：2015.11.03 10:58
    /// 描 述：TV展示屏显示内容
    /// </summary>
    public class CollectionEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        ///  主键
        /// </summary>		
        public string  Id { get; set; }
        /// <summary>
        /// 标题/姓名       
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// TV屏菜单id
        /// </summary>		
        public string TVShowMenuId { get; set; }
        /// <summary>
        /// 父级主键
        /// </summary>		
        public string ParentId { get; set; }

        /// <summary>
        /// 头像
        /// </summary>		
        public string HeadIcon { get; set; }

        /// <summary>
        /// 姓名拼音
        /// </summary>		
        public string Spelling { get; set; }
        /// <summary>
        /// 简拼
        /// </summary>		
        public string SimpleSpelling { get; set; }
        /// <summary>
        /// 首字母
        /// </summary>	
        public string Initials { get; set; }
        
        /// <summary>
        /// 性别
        /// </summary>		
        public int? Gender { get; set; }
        /// <summary>
        /// 生日
        /// </summary>		
        public DateTime? Birthday { get; set; }
        /// <summary>
        /// 手机
        /// </summary>		
        public string Mobile { get; set; }
        /// <summary>
        /// 电话
        /// </summary>		
        public string Telephone { get; set; }
        /// <summary>
        /// 电子邮件
        /// </summary>		
        public string Email { get; set; }
        /// <summary>
        /// QQ号
        /// </summary>		
        public string OICQ { get; set; }
        /// <summary>
        /// 微信号
        /// </summary>		
        public string WeChat { get; set; }
        /// <summary>
        /// 封面图
        /// </summary>		
        public string Cover { get; set; }
        

        /// <summary>
        /// 机构主键
        /// </summary>		
        public string OrganizeId { get; set; } 
        
        
        /// <summary>
        /// 籍贯
        /// </summary>		
        public string Native { get; set; } 

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
        /// 备注
        /// </summary>		
        public string Description { get; set; }
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
        /// 微信OpenId
        /// </summary>
        public string OpenId { get; set; } 

        #region MyRegion 新增字段
        /// <summary>
        /// 民族
        /// </summary>
        public string Nation { get; set; }

        /// <summary>
        /// 身份
        /// </summary>
        public string Identity { get; set; }
        /// <summary>
        /// 党派
        /// </summary>
        public string Party { get; set; } 
       
        
        /// <summary>
        /// 岗位
        /// </summary>
        public string Post { get; set; }
        /// <summary>
        /// 职位/职务
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// 工作单位
        /// </summary>
        public string WorkUnit { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string ContactAddress { get; set; }

        /// <summary>
        ///公用字段 内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        ///公用字段 建议
        /// </summary>
        public string Content1 { get; set; }
        /// <summary>
        ///公用字段 办理情况
        /// </summary>
        public string Content2 { get; set; }
        /// <summary>
        /// 公用字段
        /// </summary>
        public string ObjectName { get; set; }
        /// <summary>
        /// 公用字段
        /// </summary>
        public string ObjectName1 { get; set; }
        /// <summary>
        /// 公用字段
        /// </summary>
        public string ObjectName2 { get; set; }
        /// <summary>
        /// 邮政编码
        /// </summary>
        public string PostalCode { get; set; }
        
        /// <summary>
        /// 浏览量
        /// </summary>
        public int? PV { get; set; }
        /// <summary>
        /// 点赞量
        /// </summary>
        public int? LikeCount { get; set; }
        /// <summary>
        /// 群众满意度 打分
        /// </summary>
        public int? Flag { get; set; } 
        /// <summary>
        /// 代表名称
        /// </summary>

        public string RepresentName { get; set; }

        /// <summary>
        /// 所属类别
        /// </summary>		
        public string Category { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>		
        public DateTime? ReleaseTime { get; set; }
        /// <summary>
        /// 标题颜色
        /// </summary>		
        public string TitleColor { get; set; }
        #endregion






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
            this.DeleteMark = 0;
            this.EnabledMark = 1;
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