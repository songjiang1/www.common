using sys.Dal.Entity.AppManage; 
using sys.Dal.IService.AppManage; 
using sys.Dal.Service.AppManage; 
using sys.Util;
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
    public class SurveyAnswerBaseBLL
    {
        private ISurveyAnswerBaseService service = new SurveyAnswerBaseService();

        #region 获取数据
      
        /// <summary>
        /// 获取功能列表
        /// </summary>
        /// <param name="parentId">父级主键</param>
        /// <returns></returns>
        public List<SurveyAnswerBaseEntity> GetList(string parentId = "")
        {
            var data = service.GetList().ToList();
           
            return data;
        }
        /// <summary>
        /// 获取功能实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public SurveyAnswerBaseEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
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
        /// <param name="surveyAnswerBaseEntity">功能实体</param>
        /// <param name="surveyAnswerDetailListJson">答案列表</param> 
        /// <returns></returns>
        public void SaveForm(string keyValue, SurveyAnswerBaseEntity surveyAnswerBaseEntity, string surveyAnswerDetailListJson)
        {
            try
            {
                var surveyAnswerDetailList = surveyAnswerDetailListJson.ToList<SurveyAnswerDetailEntity>(); 
                service.SaveForm(keyValue, surveyAnswerBaseEntity, surveyAnswerDetailList);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
