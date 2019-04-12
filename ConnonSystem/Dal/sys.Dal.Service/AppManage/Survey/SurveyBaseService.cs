 
using sys.Bll.Repository;
using sys.Dal.Entity.AppManage;
using sys.Dal.IService.AppManage;
using sys.Util;
using sys.Util.Extension;
using sys.Util.WebControl;
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
    public class SurveyBaseService : RepositoryFactory<SurveyBaseEntity>, ISurveyBaseService
    {
        #region 获取数据
        /// <summary>
        /// 问卷列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<SurveyBaseEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<SurveyBaseEntity>();
            var newTime = DateTime.Now;
            var queryParam = queryJson.ToJObject();
            if (!queryParam["Title"].IsEmpty())
            {
                string Title = queryParam["Title"].ToString();
                expression = expression.And(t => t.Title.Contains(Title));
            }
            if (!queryParam["State"].IsEmpty())
            {
                if (queryParam["State"].ToString().Trim() == "0")
                {
                    expression = expression.And(t => t.OperateSDate >= newTime);
                }
                else
                {
                    expression = expression.And(t => t.OperateEDate < newTime);
                }
            }

            return this.BaseRepository().FindList(expression, pagination);
        }

        /// <summary>
        /// 功能列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SurveyBaseEntity> GetList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM b_survey_base Order By SortCode");
            return this.BaseRepository().FindList(strSql.ToString());
        }
        /// <summary>
        /// 功能实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public SurveyBaseEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region 验证数据
         
        /// <summary>
        /// 功能名称不能重复
        /// </summary>
        /// <param name="fullName">名称</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public bool ExistFullName(string fullName, string keyValue)
        {
            var expression = LinqExtensions.True<SurveyBaseEntity>();
            expression = expression.And(t => t.Title == fullName);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.Id != keyValue);
            }
            return this.BaseRepository().IQueryable(expression).Count() == 0 ? true : false;
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// +1操作
        /// </summary>
        /// <param name="keyValue"></param>
        public void PlusOne(string keyValue,OperatType operatType)
        {
            SurveyBaseEntity surveyBaseEntity = this.BaseRepository().FindEntity(keyValue);
            surveyBaseEntity.Id = keyValue;
            if (operatType == OperatType.PV)
            { 
                surveyBaseEntity.PV = surveyBaseEntity.PV + 1;
            }
            if (operatType == OperatType.JoinCount)
            {
                surveyBaseEntity.JoinCount = surveyBaseEntity.JoinCount + 1;
            }
            this.BaseRepository().Update(surveyBaseEntity);
        }
        /// <summary>
        /// 删除功能
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                
                db.Delete<SurveyBaseEntity>(keyValue);
                db.Delete<SurveyQuestionEntity>(t => t.SurveyId.Equals(keyValue));
                db.Delete<SurveyOptionsEntity>(t => t.SurveyId.Equals(keyValue));

                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }
        /// <summary>
        /// 保存表单
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="surveyBaseEntity">功能实体</param>
        /// <param name="surveyQuestionList">问题实体列表</param>
        /// <param name="surveyOptionsList">选项实体列表</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, SurveyBaseEntity surveyBaseEntity, List<SurveyQuestionEntity> surveyQuestionList, List<SurveyOptionsEntity> surveyOptionsList)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    surveyBaseEntity.Modify(keyValue);
                    db.Update(surveyBaseEntity);
                }
                else
                {
                    surveyBaseEntity.Create();
                    keyValue = surveyBaseEntity.Id;
                    db.Insert(surveyBaseEntity);
                }
                DateTime nowTime= DateTime.Now;
                for (int i = 0; i < surveyQuestionList.Count; i++)
                {
                    surveyQuestionList[i].SurveyId = keyValue;
                    surveyQuestionList[i].CreateDate = nowTime;
                }
                for (int j = 0; j < surveyOptionsList.Count; j++)
                {
                    surveyOptionsList[j].SurveyId = keyValue;
                    surveyOptionsList[j].CreateDate = nowTime;
                }
                db.Delete<SurveyOptionsEntity>(t => t.SurveyId.Equals(keyValue));
                if (surveyQuestionList != null)
                {
                    db.Insert(surveyQuestionList);
                }
                db.Delete<SurveyOptionsEntity>(t => t.SurveyId.Equals(keyValue));
                if (surveyOptionsList != null)
                {
                    db.Insert(surveyOptionsList);
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
