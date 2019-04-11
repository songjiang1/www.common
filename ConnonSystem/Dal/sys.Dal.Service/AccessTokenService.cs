using sys.Bll.Repository;
using sys.Dal.Entity;
using sys.Dal.IService;
using sys.Util;
using sys.Util.Extension;
using sys.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sys.Dal.Service
{
   public class AccessTokenService : RepositoryFactory<AccessTokenEntity>, IAccessTokenService
    {
        #region MyRegion 获取数据
        /// <summary>
        /// 验证信息列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<AccessTokenEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<AccessTokenEntity>();
            var queryParam = queryJson.ToJObject();
            
            return this.BaseRepository().FindList(expression, pagination);
        }
        /// <summary>
        /// 验证/通知消息实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public AccessTokenEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetTable()
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT Token,Timestamp   FROM b_access_token");
            return this.BaseRepository().FindTable(strSql.ToString());
        }

        #endregion
        #region MyRegion 提交数据
        /// <summary>
        /// 提交数据
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="notify"></param>
        public void SaveForm(string keyValue, AccessTokenEntity token)
        {

            if (!string.IsNullOrEmpty(keyValue))
            {
                token.Modify(keyValue);
                this.BaseRepository().Update(token);
            }
            else
            {

                token.Create();
                this.BaseRepository().Insert(token);
            }
        }
      
        #endregion
    }
}
