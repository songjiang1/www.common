using sys.Dal.Entity;
using sys.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sys.Dal.IService
{
    /// <summary>
    /// 版本 2.0
    /// Copyright (c)  
    /// 创建人：宋江
    /// 日 期：2019.3.27
    /// 描 述：验证码
    /// </summary>
    public interface INotifyService
    {

        #region 获取数据
        /// <summary>
        /// 规则列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<NotifyEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 规则实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        NotifyEntity GetEntity(string keyValue);

        /// <summary>
        /// 检查验证码是否正确
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        bool CheckNotify(string mobile,string code);
        #endregion
        #region 提交数据 
        /// <summary>
        /// 提交/修改
        /// </summary>
        ///   <param name="keyValue">主键</param>
        /// <param name="notify">对象</param>
        void SaveForm(string keyValue, NotifyEntity notify);
        /// <summary>
        /// 更新短信状态
        /// </summary>
        /// <param name="mobile"></param>
        bool UpdateNotify(string mobile);
        #endregion
    }
}
