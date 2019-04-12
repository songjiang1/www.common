 
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
    /// 描 述：系统答案详情
    /// </summary>
    public class SurveyAnswerDetailService : RepositoryFactory<SurveyAnswerDetailEntity>, ISurveyAnswerDetailService
    {
        #region 获取数据
        /// <summary>
        /// 答案详情列表
        /// </summary>
        /// <returns></returns>
        public List<SurveyAnswerDetailEntity> GetList()
        {
            return this.BaseRepository().IQueryable().ToList();
        }
        /// <summary>
        /// 答案详情列表
        /// </summary>
        /// <param name="moduleId">功能Id</param>
        /// <returns></returns>
        public List<SurveyAnswerDetailEntity> GetList(string Id)
        {
            var expression = LinqExtensions.True<SurveyAnswerDetailEntity>();
            expression = expression.And(t => t.AnswersBaseId.Equals(Id));
            return this.BaseRepository().IQueryable(expression).ToList();
        }
        /// <summary>
        /// 答案详情实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public SurveyAnswerDetailEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 添加答案详情
        /// </summary>
        /// <param name="surveyAnswerDetailEntity">答案详情实体</param>
        public void AddEntity(SurveyAnswerDetailEntity surveyAnswerDetailEntity)
        {
            surveyAnswerDetailEntity.Create();
            this.BaseRepository().Insert(surveyAnswerDetailEntity);
        }
        #endregion
    }
}
