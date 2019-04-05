using sys.Dal.Entity.AppManage;
using sys.Dal.IService.AppManage;
using sys.Dal.Service.AppManage;
using sys.Util;
using sys.Util.Extension;
using System;
using System.Collections.Generic;

namespace sys.Dal.Busines.AppManage
{
    /// <summary>
    /// 版 本 2.0
    /// Copyright (c)  
    /// 创建人：宋江
    /// 日 期：2015.12.8 11:31
    /// 描 述：邮件分类
    /// </summary>
    public class MessageReadBLL
    {
        private IMessageReadService service = new MessageReadService();

        #region 获取数据
        /// <summary>
        /// 分类列表
        /// </summary>
        /// <param name="UserId">用户Id</param>
        /// <returns></returns>
        public IEnumerable<MessageReadEntity> GetList(string UserId)
        {
            return service.GetList(UserId);
        }
        /// <summary>
        /// 分类实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public MessageReadEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="oid">关联主键id</param>
        /// <param name="category">分类</param>
        /// <returns></returns>
        public MessageReadEntity GetEntity(string uid, string oid, string category)
        {
            return service.GetEntity(uid,oid,category);

        }

        /// <summary>
        /// 判断用户是否存在
        /// </summary>   ExistUser
        /// <param name="messageId">关联主键</param>
        /// <param name="userId">公司名称</param>
        /// <param name="type">分类</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public bool ExistUser(string messageId, string userId,string type, string keyValue)
        {
            return service.ExistUser(messageId, userId, type, keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除分类
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            try
            {
                service.RemoveForm(keyValue);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="emailCategoryEntity">分类实体</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, MessageReadEntity messageReadEntity)
        {
            try
            {
                service.SaveForm(keyValue, messageReadEntity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// g更新
        /// </summary>
        /// <param name="uid">用户主键</param>
        /// <param name="oid">关联主键</param>
        /// <param name="category">类型</param>
        /// <param name="operatType"></param>
        public void SetForm(string uid,string oid,string category, OperatType operatType)
        {
            try
            {
                service.SetForm(uid, oid, category, operatType);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 签到
        /// </summary>
        /// <param name="uid">用户主键</param>
        /// <param name="oid">关联主键</param>
        /// <param name="category">类型</param>
        /// <param name="operatType"></param>
        public int SignInMark(string uid, string oid, string category, OperatType operatType,string SignInDescription)
        {
            try
            {
              return  service.SignInMark(uid, oid, category, operatType, SignInDescription);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
