
using sys.Dal.Entity.AppManage;
using sys.Dal.IService.AppManage;
using sys.Dal.Service.AppManage;
using sys.Util;
using sys.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;

namespace sys.Dal.Busines.AppManage
{
    /// <summary>
    /// 版 本 2.0
    /// Copyright (c)  
    /// 创建人：宋江
    /// 日 期：2015.10.27 09:16
    /// 描 述：系统功能
    /// </summary>
    public class SurveyBaseBLL
    {
        private ISurveyBaseService service = new SurveyBaseService();

        #region 获取数据

        /// <summary>
        /// 会议列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<SurveyBaseEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        /// <summary>
        /// 获取功能列表
        /// </summary>
        /// <param name="parentId">父级主键</param>
        /// <returns></returns>
        public List<SurveyBaseEntity> GetList()
        {
            var data = service.GetList().ToList(); 
            return data;
        }
        /// <summary>
        /// 获取功能实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public SurveyBaseEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        /// <summary>
        /// +1操作 
        /// </summary>
        /// <param name="keyValue"></param>
        public void PlusOne(string keyValue, OperatType operatType)
        {
            try
            {
                service.PlusOne(keyValue, operatType);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region 验证数据

        /// <summary>
        /// 问卷名称不能重复
        /// </summary>
        /// <param name="fullName">名称</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public bool ExistFullName(string fullName, string keyValue)
        {
            return service.ExistFullName(fullName, keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除功能
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            try
            {
                service.RemoveForm(keyValue);
            }
            catch (Exception)
            {
                throw;
            }

        }
        /// <summary>
        /// 保存表单
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="surveyBaseEntity">功能实体</param>
        /// <param name="surveyQuestionListJson">问题实体列表</param>
        /// <param name="surveyOptionsListJson">选项实体列表</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, SurveyBaseEntity surveyBaseEntity, string surveyQuestionListJson, string surveyOptionsListJson)
        {
            try
            {
                var surveyOptionsList = surveyOptionsListJson.ToList<SurveyOptionsEntity>();
                var surveyQuestionList = surveyQuestionListJson.ToList<SurveyQuestionEntity>();
                service.SaveForm(keyValue, surveyBaseEntity, surveyQuestionList, surveyOptionsList);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
