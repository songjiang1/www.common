using sys.Dal.Entity; 
using sys.Dal.IService;
using sys.Dal.Service;
using sys.Util.WebControl;
using System;
using System.Collections.Generic;

namespace sys.Dal.Busines
{
    /// <summary>
    /// 版 本 2.0
    /// Copyright (c)  
    /// 创建人：宋江
    /// 日 期：2019
    /// 描 述：短信通知
    /// </summary>
    public class NotifyBLL
    {
        private INotifyService service = new NotifyService(); 
        #region 获取数据
        /// <summary>
        /// 分类列表
        /// </summary>
        /// <param name="UserId">用户Id</param>
        /// <returns></returns>
        public IEnumerable<NotifyEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        /// <summary>
        /// 分类实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public NotifyEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        public bool CheckNotify(string mobile, string code)
        {
            return service.CheckNotify(mobile,code);
        }
        
        #endregion

        #region 提交数据

        /// <summary>
        /// 保存分类表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="notify">分类实体</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, NotifyEntity notify)
        {
            try
            {
                service.SaveForm(keyValue, notify);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 更新短信状态
        /// </summary>
        /// <param name="mobile"></param>
        ///   
        public bool UpdateNotify(string mobile)
        {
            try
            {
              return  service.UpdateNotify(mobile);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
