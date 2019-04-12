using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sys.Application.app.Models
{
    public class SurveyBase
    {
        #region 实体成员
        /// <summary>
        /// 主键
        /// </summary>		
        public string Id { get; set; }
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
        /// 浏览量数量
        /// </summary>		
        public int? PV { get; set; }

        /// <summary>
        /// 参与人员数量
        /// </summary>		
        public int? JoinCount { get; set; }

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

        public List<SurveyQuestion> SurveyQuestionList { get; set; }

        #endregion
    }
    public class SurveyQuestion
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
        ///答案 没有选项问题的答案，如：描述，填空  仅展示数据使用
        /// </summary>	
        public string Content { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>		
        public DateTime? CreateDate { get; set; }

        public List<SurveyOptions> SurveyOptionsList { get; set; }
        #endregion
    }

    public class SurveyOptions
    {
        #region 实体成员
        /// <summary>
        /// 主键
        /// </summary>		
        public string OptionId { get; set; }
        /// <summary>
        /// 问卷主键
        /// </summary>		
        public string SurveyId { get; set; }

        /// <summary>
        /// 问题主键
        /// </summary>		
        public string QuestionId { get; set; }
        /// <summary>
        /// 内容
        /// </summary>		
        public string Content { get; set; }
        /// <summary>
        /// 分类 单选，多选，描述
        /// </summary>		
        public string Category { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>	
        public string SortCode { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>		
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// 是否答案 仅数据展示使用字段
        /// </summary>
        public bool IsAnswer { get; set; }
        #endregion
    }


    public class SurveyAnswerDetail
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
    }
}