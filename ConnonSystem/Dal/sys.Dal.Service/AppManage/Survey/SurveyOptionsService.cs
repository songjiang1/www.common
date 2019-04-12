 
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
    /// 描 述：系统选项
    /// </summary>
    public class SurveyOptionsService : RepositoryFactory<SurveyOptionsEntity>, ISurveyOptionsService
    {
        #region 获取数据
        /// <summary>
        /// 选项列表
        /// </summary>
        /// <returns></returns>
        public List<SurveyOptionsEntity> GetList()
        {
            return this.BaseRepository().IQueryable().OrderBy(t => t.SortCode).ToList();
        }
        /// <summary>
        /// 选项列表
        /// </summary>
        /// <param name="moduleId">问题Id</param>
        /// <returns></returns>
        public List<SurveyOptionsEntity> GetList(string Id)
        {
            var expression = LinqExtensions.True<SurveyOptionsEntity>();
            expression = expression.And(t => t.QuestionId.Equals(Id));
            return this.BaseRepository().IQueryable(expression).OrderBy(t => t.SortCode).ToList();
        }
        /// <summary>
        /// 选项实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public SurveyOptionsEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 添加选项
        /// </summary>
        /// <param name="surveyOptionsEntity">选项实体</param>
        public void AddEntity(SurveyOptionsEntity surveyOptionsEntity)
        {
            surveyOptionsEntity.Create();
            this.BaseRepository().Insert(surveyOptionsEntity);
        }
        #endregion
    }
}
