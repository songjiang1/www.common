using sys.Dal.Entity.AppManage;
using System.Collections.Generic;

namespace sys.Dal.IService.AppManage
{
    /// <summary>
    /// 版 本 2.0
    /// Copyright (c)  
    /// 创建人：宋江
    /// 日 期：2015.08.01 14:00
    /// 描 述：问卷调查 选项表
    /// </summary>
    public interface ISurveyOptionsService
    {
        #region 获取数据
        /// <summary>
        /// 选项列表
        /// </summary>
        /// <returns></returns>
        List<SurveyOptionsEntity> GetList();
        /// <summary>
        /// 选项列表
        /// </summary>
        /// <param name="moduleId">功能Id</param>
        /// <returns></returns>
        List<SurveyOptionsEntity> GetList(string moduleId);
        /// <summary>
        /// 选项实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        SurveyOptionsEntity GetEntity(string keyValue);
        #endregion

        #region 提交数据
        /// <summary>
        /// 添加选项
        /// </summary>
        /// <param name="moduleButtonEntity">选项实体</param>
        void AddEntity(SurveyOptionsEntity surveyOptionsEntity);
        #endregion
    }
}
