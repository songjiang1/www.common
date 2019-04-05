using sys.Dal.Entity.AppManage;
using sys.Util;
using System.Collections.Generic;

namespace sys.Dal.IService.AppManage
{
    /// <summary>
    /// 版 本 2.0
    /// Copyright (c)  
    /// 创建人：宋江
    /// 日 期：2015.12.8 11:31
    /// 描 述：邮件分类
    /// </summary>
    public interface IMessageReadService
    {
        #region 获取数据
        /// <summary>
        /// 分类列表
        /// </summary>
        /// <param name="UserId">用户Id</param>
        /// <returns></returns>
        IEnumerable<MessageReadEntity> GetList(string UserId);
        /// <summary>
        /// 分类实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        MessageReadEntity GetEntity(string keyValue);

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="oid">关联主键id</param>
        /// <param name="category">分类</param>
        /// <returns></returns>
        MessageReadEntity GetEntity(string uid, string oid, string category);

        /// <summary>
        /// 判断用户是否存在
        /// </summary>   ExistUser
        /// <param name="messageId">关联主键</param>
        /// <param name="userId">公司名称</param>
        /// <param name="type">分类</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns> 
        bool ExistUser(string messageId, string userId, string type, string keyValue);
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除分类
        /// </summary>
        /// <param name="keyValue">主键</param>
        void RemoveForm(string keyValue);
        /// <summary>
        /// 保存分类表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="emailCategoryEntity">分类实体</param>
        /// <returns></returns>
        void SaveForm(string keyValue, MessageReadEntity messageReadEntity);
        /// g更新操作
        /// </summary>
        /// <param name="uid">用户主键</param>
        /// <param name="oid">关联主键</param>
        /// <param name="category">类型</param>
        /// <param name="operatType"></param>
       void SetForm(string uid, string oid, string category, OperatType operatType);
        /// <summary>
        /// 签到
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="oid"></param>
        /// <param name="category"></param>
        /// <param name="operatType"></param>
        /// <returns></returns>
        int SignInMark(string uid, string oid, string category, OperatType operatType,string SignInDescription);
        #endregion
    }
}
