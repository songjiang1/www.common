 
using sys.Bll.Repository;
using sys.Dal.Entity.AppManage;
using sys.Dal.IService.AppManage;
using sys.Util.Extension;
using System.Collections.Generic;
using System.Linq;

namespace sys.Dal.Service.AppManage
{
    /// <summary>
    /// 版 本 2.0
    /// Copyright (c)  
    /// 创建人：宋江
    /// 日 期：2015.08.01 14:00
    /// 描 述：系统问题
    /// </summary>
    public class  SurveyQuestionService : RepositoryFactory<SurveyQuestionEntity>, ISurveyQuestionService
    {
        #region 获取数据
        /// <summary>
        /// 问题列表
        /// </summary>
        /// <returns></returns>
        public List<SurveyQuestionEntity> GetList()
        {
            return this.BaseRepository().IQueryable().OrderBy(t => t.SortCode).ToList();
        }
        /// <summary>
        /// 问题列表
        /// </summary>
        /// <param name="moduleId">功能Id</param>
        /// <returns></returns>
        public List<SurveyQuestionEntity> GetList(string Id)
        {
            var expression = LinqExtensions.True<SurveyQuestionEntity>();
            expression = expression.And(t => t.SurveyId.Equals(Id));
            return this.BaseRepository().IQueryable(expression).OrderBy(t => t.SortCode).ToList();
        }
        /// <summary>
        /// 问题实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public SurveyQuestionEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 添加问题
        /// </summary>
        /// <param name="moduleButtonEntity">问题实体</param>
        public void AddEntity(SurveyQuestionEntity surveyQuestionEntity)
        {
            surveyQuestionEntity.Create();
            this.BaseRepository().Insert(surveyQuestionEntity);
        }
        #endregion
    }
}
