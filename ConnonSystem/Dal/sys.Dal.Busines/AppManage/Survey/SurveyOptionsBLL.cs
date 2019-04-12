
using sys.Dal.Entity.AppManage;
using sys.Dal.IService.AppManage;
using sys.Dal.Service.AppManage;
using System;
using System.Collections.Generic;

namespace sys.Dal.Busines.AppManage
{
    /// <summary>
    /// 版 本 2.0
    /// Copyright (c)  
    /// 创建人：宋江
    /// 日 期：2015.08.01 14:00
    /// 描 述：系统按钮
    /// </summary>
    public class SurveyOptionsBLL
    {
        private ISurveyOptionsService service = new SurveyOptionsService();

        #region 获取数据
        /// <summary>
        /// 按钮列表
        /// </summary>
        /// <returns></returns>
        public List<SurveyOptionsEntity> GetList()
        {
            return service.GetList();
        }
        /// <summary>
        /// 按钮列表
        /// </summary>
        /// <param name="moduleId">功能Id</param>
        /// <returns></returns>
        public List<SurveyOptionsEntity> GetList(string moduleId)
        {
            return service.GetList(moduleId);
        }
        /// <summary>
        /// 按钮实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public SurveyOptionsEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 复制按钮
        /// </summary>
        /// <param name="KeyValue">主键</param>
        /// <param name="moduleId">功能主键</param>
        /// <returns></returns>
        public void CopyForm(string keyValue, string Id)
        {
            try
            {
                SurveyOptionsEntity surveyOptionsEntity = this.GetEntity(keyValue);
                surveyOptionsEntity.OptionId = Id;
                service.AddEntity(surveyOptionsEntity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
