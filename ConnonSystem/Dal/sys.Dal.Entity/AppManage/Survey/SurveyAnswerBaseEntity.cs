using sys.Aplication.Code;
using System;

namespace sys.Dal.Entity.AppManage
{
    /// <summary>
    /// 版 本 1.0
    /// Copyright (c)  
    /// 创建人：宋江
    /// 日 期：2015.12.7 16:40
    /// 描 述：问卷调查 答案表
    /// </summary>
    public class SurveyAnswerBaseEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 主键
        /// </summary>		
        public string  Id { get; set; }
        /// <summary>
        /// 问卷主键
        /// </summary>		
        public string SurveyId { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>		
        public string UserId { get; set; }
        
        /// <summary>
        /// 用户名称
        /// </summary>		
        public string UserName { get; set; }
       
        /// <summary>
        /// 创建时间
        /// </summary>		
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>		
        public DateTime? ModifyDate { get; set; }




        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreateDate = DateTime.Now;
            this.UserId = OperatorProvider.Provider.Current().UserId;
            this.UserName = OperatorProvider.Provider.Current().UserName; 
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.Id = keyValue;
            this.ModifyDate = DateTime.Now;
            this.UserId = OperatorProvider.Provider.Current().UserId;
            this.UserName = OperatorProvider.Provider.Current().UserName;
        }
        #endregion
    }
}
