
using sys.Dal.Entity.AppManage;
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
    public interface ISurveyAnswerBaseService
    {
        #region 获取数据 
        /// <summary>
        /// 功能列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<SurveyAnswerBaseEntity> GetList();
        /// <summary>
        /// 功能实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        SurveyAnswerBaseEntity GetEntity(string keyValue);
        #endregion

         

        #region 提交数据
        /// <summary>
        /// 删除功能
        /// </summary>
        /// <param name="keyValue">主键</param>
        void RemoveForm(string keyValue);
        /// <summary>
        /// 保存表单（新增、修改）  
        /// <param name="keyValue">主键值</param>
        /// <param name="surveyAnswerBaseEntity">功能实体</param>
        /// <param name="surveyAnswerDetailListJson">答案列表</param> 
        /// <returns></returns> 
        string SaveForm(string keyValue, SurveyAnswerBaseEntity surveyAnswerBaseEntity, List<SurveyAnswerDetailEntity> surveyAnswerDetailEntity);
        #endregion
    }
}
