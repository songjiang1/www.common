using sys.Aplication.Code;
using System;

namespace sys.Dal.Entity.AppManage
{
    /// <summary>
    /// 版 本 1.0
    /// Copyright (c)  
    /// 创建人：宋江
    /// 日 期：2015.12.7 16:40
    /// 描 述：问卷调查 答案详情
    /// </summary>
    public class SurveyAnswerDetailEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 主键
        /// </summary>		
        public string AnswersDetailId { get; set; }
        /// <summary>
        /// 答案表主键
        /// </summary>		
        public string AnswersBaseId { get; set; }
        /// <summary>
        /// 问题表主键
        /// </summary>		
        public string QuestionId { get; set; }
        
        /// <summary>
        /// 选项表主键
        /// </summary>		
        public string OptionId { get; set; }
       
        /// <summary>
        /// 内容  描述，填空题
        /// </summary>		
        public string Content { get; set; } 
        /// <summary>
        /// 创建时间
        /// </summary>		
        public DateTime? CreateDate { get; set; }




        #endregion

        #region 扩展操作

        #endregion
    }
}
