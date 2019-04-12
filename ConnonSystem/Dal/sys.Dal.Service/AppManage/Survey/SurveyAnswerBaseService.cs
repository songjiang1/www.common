using sys.Bll.Repository;
using sys.Dal.Entity.AppManage;
using sys.Dal.IService.AppManage;
using sys.Util.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sys.Dal.Service.AppManage
{
    /// <summary>
    /// 版 本 2.0
    /// Copyright (c)  
    /// 创建人：宋江
    /// 日 期：2015.10.27 09:16
    /// 描 述：系统功能
    /// </summary>
    public class SurveyAnswerBaseService : RepositoryFactory<SurveyAnswerBaseEntity>, ISurveyAnswerBaseService
    {
        #region 获取数据
       
        /// <summary>
        /// 功能列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SurveyAnswerBaseEntity> GetList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM b_survey_answer_base  ");
            return this.BaseRepository().FindList(strSql.ToString());
        }
        /// <summary>
        /// 功能实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public SurveyAnswerBaseEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion

         

        #region 提交数据
        /// <summary>
        /// 删除功能
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            { 
                db.Delete<SurveyAnswerBaseEntity>(keyValue);
                db.Delete<SurveyAnswerDetailEntity>(t => t.AnswersBaseId.Equals(keyValue));  
                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }
        /// <summary> 
        /// 保存表单（新增、修改）  
        /// <param name="keyValue">主键值</param>
        /// <param name="surveyAnswerBaseEntity">功能实体</param>
        /// <param name="surveyAnswerDetailListJson">答案列表</param> 
        /// <returns></returns> 
        public void SaveForm(string keyValue, SurveyAnswerBaseEntity surveyAnswerBaseEntity, List<SurveyAnswerDetailEntity> surveyAnswerDetailEntity)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    surveyAnswerBaseEntity.Modify(keyValue);
                    db.Update(surveyAnswerBaseEntity);
                }
                else
                {
                    surveyAnswerBaseEntity.Create();
                    db.Insert(surveyAnswerBaseEntity);
                }
                db.Delete<SurveyAnswerDetailEntity>(t => t.AnswersBaseId.Equals(keyValue));
                if (surveyAnswerDetailEntity != null)
                {
                    db.Insert(surveyAnswerDetailEntity);
                }
                
                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }
        #endregion
    }
}
