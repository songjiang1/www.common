using sys.Aplication.Code;
using System;

namespace sys.Dal.Entity.AppManage
{
    /// <summary>
    /// 版 本 1.0
    /// Copyright (c)  
    /// 创建人：宋江
    /// 日 期：2015.12.7 16:40
    /// 描 述：问卷调查 内容表
    /// </summary>
    public class SurveyQuestionEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 主键
        /// </summary>		
        public string QuestionId { get; set; }
        /// <summary>
        /// 问卷主键
        /// </summary>		
        public string SurveyId { get; set; }
        /// <summary>
        /// 问题分类 单选，多选，描述
        /// </summary>		
        public string Category { get; set; }
        
        /// <summary>
        /// 问题名称
        /// </summary>		
        public string Title { get; set; }
       
        /// <summary>
        /// 排序号
        /// </summary>		
        public string SortCode { get; set; }
        
      
        /// <summary>
        /// 分值
        /// </summary>		
        public int? Score { get; set; }


        /// <summary>
        /// 创建时间
        /// </summary>		
        public DateTime? CreateDate { get; set; }

        #endregion

        #region 扩展操作

        #endregion
    }
}
