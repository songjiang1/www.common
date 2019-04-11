using sys.Dal.Entity;
using sys.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace sys.Dal.IService
{
    /// <summary>
    /// 版本 2.0
    /// Copyright (c)  
    /// 创建人：宋江
    /// 日 期：2019.3.27
    /// 描 述：验证码
    /// </summary>
    public interface IAccessTokenService
    {

        #region 获取数据
        /// <summary>
        /// 规则列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<AccessTokenEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 规则实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        AccessTokenEntity GetEntity(string keyValue);
        DataTable GetTable();
        #endregion
        #region 提交数据 
        /// <summary>
        /// 提交/修改
        /// </summary>
        ///   <param name="keyValue">主键</param>
        /// <param name="token">对象</param>
        void SaveForm(string keyValue, AccessTokenEntity token); 
        #endregion
    }
}
