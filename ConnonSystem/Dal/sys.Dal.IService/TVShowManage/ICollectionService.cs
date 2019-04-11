  using sys.Util.WebControl;
using System.Collections.Generic;
using System.Data;
using sys.Aplication.Code;
using sys.Dal.Entity.TVShowManage;

namespace sys.Dal.IService.TVShowManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c)  
    /// 创建人：宋江
    /// 日 期：2015.11.03 10:58
    /// 描 述：TV端管理
    /// </summary>
    public interface ICollectionService
    {
        #region 获取数据
        /// <summary>
        /// 数据列表 app端
        /// </summary>
        /// <returns></returns>
        DataTable GetTable(CollectionEntity collection, string OrderBy);
        /// <summary>
        /// 用户名首字母
        /// </summary>
        /// <returns></returns>
        DataTable GetInitials(CollectionEntity collection);
        /// <summary>
        ///  获取列表数据
        /// </summary>
        /// <returns></returns>
        IEnumerable<CollectionEntity> GetList();
        /// <summary>
        /// 根据条件查询 
        /// </summary>
        /// <returns></returns>
        IEnumerable<CollectionEntity> GetUserSearch(CollectionEntity collection);
        /// <summary>
        ///  列表查询
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<CollectionEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        ///  列表（ALL）
        /// </summary>
        /// <returns></returns>
        DataTable GetAllTable();
        /// <summary>
        ///  实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        CollectionEntity GetEntity(string keyValue);
     
        /// <summary>
        /// 导出用户列表
        /// </summary>
        /// <returns></returns>
        DataTable GetExportList();

        /// <summary>
        /// 查询手机号
        /// </summary>
        /// <param name="Mobile">Mobile</param>
        /// <returns></returns>
        CollectionEntity CheckMobile(string Mobile);
        #endregion

         

        #region 提交数据
        /// <summary>
        /// 删除 
        /// </summary>
        /// <param name="keyValue">主键</param>
        void RemoveForm(string keyValue);
        /// <summary>
        /// 保存 表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="userEntity">用户实体</param>
        /// <returns></returns>
        void SaveForm(string keyValue, CollectionEntity userEntity);
      
        #endregion
    }
}

