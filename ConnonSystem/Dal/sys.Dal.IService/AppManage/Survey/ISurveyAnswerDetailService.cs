using sys.Dal.Entity.AppManage;
using System.Collections.Generic;

namespace sys.Dal.IService.AppManage
{
    /// <summary>
    /// 版 本 2.0
    /// Copyright (c)  
    /// 创建人：宋江
    /// 日 期：2015.08.01 14:00
    /// 描 述：系统按钮
    /// </summary>
    public interface ISurveyAnswerDetailService
    {
        #region 获取数据
        /// <summary>
        /// 按钮列表
        /// </summary>
        /// <returns></returns>
        List<SurveyAnswerDetailEntity> GetList();
        /// <summary>
        /// 按钮列表
        /// </summary>
        /// <param name="Id">功能Id</param>
        /// <returns></returns>
        List<SurveyAnswerDetailEntity> GetList(string Id);
        /// <summary>
        /// 按钮实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        SurveyAnswerDetailEntity GetEntity(string keyValue);
        #endregion

        #region 提交数据
        /// <summary>
        /// 添加答案详情
        /// </summary>
        /// <param name="moduleButtonEntity">答案详情实体</param>
        void AddEntity(SurveyAnswerDetailEntity surveyAnswerDetailEntity);
        #endregion
    }
}
