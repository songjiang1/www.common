using sys.Dal.Entity.AppManage;
using sys.Dal.IService.AppManage;
using sys.Bll.Repository;
using sys.Util;
using sys.Util.Extension;
using sys.Util.WebControl;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;

namespace sys.Dal.Service.AppManage
{
    /// <summary>
    /// 版 本 2.0
    /// Copyright (c)  
    /// 创建人：宋江
    /// 日 期：2015.12.7 16:40
    /// 描 述：电子公告
    /// </summary>
    public class CommentService : RepositoryFactory<CommentEntity>, ICommentService
    {
        #region 获取数据
        /// <summary>
        /// 公告列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<CommentEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<CommentEntity>();
            var queryParam = queryJson.ToJObject(); 
            if (!queryParam["Category"].IsEmpty())
            {
                string Category = queryParam["Category"].ToString();
                expression = expression.And(t => t.Category == Category);
            }
            if (!queryParam["ObjectId"].IsEmpty())
            {
                string ObjectId = queryParam["ObjectId"].ToString();
                expression = expression.And(t => t.ObjectId == ObjectId);
            }
            expression = expression.And(t => t.VerifyMark == true);
            return this.BaseRepository().FindList(expression, pagination);
        }
        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CommentEntity> GetList(string objId)
        {
            return this.BaseRepository().IQueryable().Where(t=>t.VerifyMark==true && t.ObjectId==objId).OrderByDescending(t => t.CreateDate).ToList();
        }
        /// <summary>
        /// 公告实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public CommentEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataTable GetTable(Pagination pagination)
        {
            var strSql = new StringBuilder();
            strSql.Append(@" SELECT * FROM  b_comment b  WHERE 1=1  ORDER BY  " + pagination.sidx+ "  "+ pagination .sord+ "  ");
            strSql.Append(" LIMIT "+ (pagination.page - 1) * pagination.rows + ", "+ pagination.rows + " ");
            return this.BaseRepository().FindTable(strSql.ToString());
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除公告
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// 保存公告表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="newsEntity">公告实体</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, CommentEntity newsEntity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                newsEntity.Modify(keyValue); 
                this.BaseRepository().Update(newsEntity);
            }
            else
            {
                newsEntity.Create(); 
                this.BaseRepository().Insert(newsEntity);
            }
        }
         
        #endregion
    }
}
