
using sys.Dal.Entity.AppManage;
using sys.Util.WebControl;
using System.Collections.Generic;

namespace sys.Dal.IService.AppManage
{
    /// <summary>
    /// 版 本 2.0
    /// Copyright (c)  
    /// 创建人：宋江
    /// 日 期：2015.10.27 09:16
    /// 描 述：系统功能
    /// </summary>
    public interface ISurveyBaseService
    {
        #region 获取数据
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<SurveyBaseEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 功能列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<SurveyBaseEntity> GetList();
        /// <summary>
        /// 功能实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        SurveyBaseEntity GetEntity(string keyValue);
        #endregion

        #region 验证数据
        
        /// <summary>
        /// 功能名称不能重复
        /// </summary>
        /// <param name="fullName">名称</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        bool ExistFullName(string fullName, string keyValue);
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除功能
        /// </summary>
        /// <param name="keyValue">主键</param>
        void RemoveForm(string keyValue);
        /// <summary>
        /// 保存表单
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="surveyBaseEntity">功能实体</param>
        /// <param name="surveyQuestionListJson">问题实体列表</param>
        /// <param name="surveyOptionsListJson">选项实体列表</param>
        /// <returns></returns>
        void SaveForm(string keyValue, SurveyBaseEntity surveyBaseEntity, List<SurveyQuestionEntity> surveyQuestionListJson, List<SurveyOptionsEntity> surveyOptionsListJson);
        #endregion
    }
}
