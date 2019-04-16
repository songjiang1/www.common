using sys.Aplication.Code;
using System;

namespace sys.Dal.Entity.AppManage
{
    /// <summary>
    /// 版 本 2.0
    /// Copyright (c)  
    /// 创建人：宋江
    /// 日 期：2015.12.7 16:40
    /// 描 述：评论
    /// </summary>
    public class CommentEntity : BaseEntity
    {
        #region 实体成员

        /// <summary>
        /// 主键
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 对象主键
        /// </summary>
        public string ObjectId { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        public string Category { get; set; }
  
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateDate { get; set; }
        public string CreateUserId { get; set; }
        public string CreateUserName { get; set; }
        public bool VerifyMark { get; set; }
        public DateTime? VerifyDate { get; set; }
        public string VerifyDescribe { get; set; }
        public string CreateUserHeadIcon { get; set; }
        public string Content { get; set; }

        public string CreateUserPosition { get; set; }
        


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
            this.VerifyMark = false;
            this.CreateUserHeadIcon = OperatorProvider.Provider.Current().HeadIcon;
            this.CreateUserPosition = OperatorProvider.Provider.Current().Position;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.Id = keyValue; 
        }
        #endregion
    }
}
