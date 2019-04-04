using sys.Dal.Entity.AppManage;
using sys.Util.WebControl;
using System.Collections.Generic;
using System.Data;

namespace sys.Dal.IService.AppManage
{
    /// <summary>
    /// 版 本 2.0
    /// Copyright (c)  
    /// 创建人：宋江
    /// 日 期：2015.12.7 16:40
    /// 描 述：电子公告
    /// </summary>
    public interface INoticeService
    {
        #region 获取数据
        /// <summary>
        /// 公告列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<NoticeEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 公告实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        NoticeEntity GetEntity(string keyValue);
        /// <summary>
        /// 获取数据 app段展示
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        DataTable GetTable(Pagination pagination);
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除公告
        /// </summary>
        /// <param name="keyValue">主键</param>
        void RemoveForm(string keyValue);
        /// <summary>
        /// 保存公告表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="newsEntity">公告实体</param>
        /// <returns></returns>
        void SaveForm(string keyValue, NoticeEntity newsEntity);

        /// <summary>
        /// 更新浏览量
        /// </summary>
        /// <param name="keyValue"></param>
        void PvPlusOne(string keyValue);
        #endregion
    }
}
