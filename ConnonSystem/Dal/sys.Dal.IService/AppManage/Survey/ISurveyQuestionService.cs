using sys.Dal.Entity.AppManage;
using System.Collections.Generic;

namespace sys.Dal.IService.AppManage
{
    /// <summary>
    /// 版 本 2.0
    /// Copyright (c)  
    /// 创建人：宋江
    /// 日 期：2015.08.01 14:00
    /// 描 述：问卷调查 问题表
    /// </summary>
    public interface ISurveyQuestionService
    {
        #region 获取数据
        /// <summary>
        /// 问题列表
        /// </summary>
        /// <returns></returns>
        List<SurveyQuestionEntity> GetList();
        /// <summary>
        /// 问题列表
        /// </summary>
        /// <param name="moduleId">功能Id</param>
        /// <returns></returns>
        List<SurveyQuestionEntity> GetList(string moduleId);
        /// <summary>
        /// 问题实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        SurveyQuestionEntity GetEntity(string keyValue);
        #endregion

        #region 提交数据
        /// <summary>
        /// 添加问题
        /// </summary>
        /// <param name="surveyQuestionEntity">问题实体</param>
        void AddEntity(SurveyQuestionEntity surveyQuestionEntity);
        #endregion
    }
}
