using sys.Dal.Entity; 
using sys.Dal.IService;
using sys.Dal.Service;
using sys.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace sys.Dal.Busines
{
    /// <summary>
    /// 版 本 2.0
    /// Copyright (c)  
    /// 创建人：宋江
    /// 日 期：2019
    /// 描 述：短信通知
    /// </summary>
    public class AccessTokenBLL
    {
        private IAccessTokenService service = new AccessTokenService();
        #region 获取数据

        public DataTable GetTable()
        {
            return service.GetTable();
        }
        /// <summary>
        /// 分类列表
        /// </summary>
        /// <param name="UserId">用户Id</param>
        /// <returns></returns>
        public IEnumerable<AccessTokenEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        /// <summary>
        /// 分类实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public AccessTokenEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }


        #endregion

        #region 提交数据

        /// <summary>
        /// 保存分类表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="token">分类实体</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, AccessTokenEntity token)
        {
            try
            {
                service.SaveForm(keyValue, token);
            }
            catch (Exception)
            {
                throw;
            }
        }

        
        #endregion
    }
}
