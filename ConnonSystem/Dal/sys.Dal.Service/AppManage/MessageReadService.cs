using sys.Dal.Entity.AppManage;
using sys.Dal.IService.AppManage;
using sys.Bll.Repository;
using sys.Util.Extension;
using System.Collections.Generic;
using System.Linq;
using sys.Util;
using System;

namespace sys.Dal.Service.AppManage
{
    /// <summary>
    /// 版 本 2.0
    /// Copyright (c)  
    /// 创建人：宋江
    /// 日 期：2015.12.8 11:31
    /// 描 述：邮件分类
    /// </summary>
    public class MessageReadService : RepositoryFactory<MessageReadEntity>, IMessageReadService
    {
        #region 获取数据
        /// <summary>
        /// 分类列表
        /// </summary>
        /// <param name="UserId">用户Id</param>
        /// <returns></returns>
        public IEnumerable<MessageReadEntity> GetList(string UserId)
        {
            var expression = LinqExtensions.True<MessageReadEntity>();
            if (!string.IsNullOrWhiteSpace(UserId))
            {
                expression.And(t => t.UserId == UserId);
            }
            return this.BaseRepository().IQueryable(expression).ToList();
        }
        /// <summary>
        ///  获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public MessageReadEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
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
            MessageReadEntity messageReadEntity = new MessageReadEntity();
            var expression = LinqExtensions.True<MessageReadEntity>();
            expression = expression.And(t => t.UserId == uid && t.MessageId == oid && t.Category == category);
            return this.BaseRepository().FindEntity(expression);
           
        }
        /// <summary>
        /// 判断用户是否存在
        /// </summary>   ExistUser
        /// <param name="messageId">关联主键</param>
        /// <param name="userId">公司名称</param>
        /// <param name="type">分类</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns> 
        public bool ExistUser(string messageId, string userId, string type, string keyValue)
        {
            var expression = LinqExtensions.True<MessageReadEntity>();
            expression = expression.And(t => t.MessageId == messageId && t.UserId == userId && t.Category == type);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.Uniqueid != keyValue);
            }
            return this.BaseRepository().IQueryable(expression).Count() == 0 ? true : false;
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除分类
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// 保存分类表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="emailCategoryEntity">分类实体</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, MessageReadEntity messageReadEntity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                messageReadEntity.Modify(keyValue);
                this.BaseRepository().Update(messageReadEntity);
            }
            else
            {
                messageReadEntity.Create();
                messageReadEntity.Uniqueid = Guid.NewGuid().ToString();
                this.BaseRepository().Insert(messageReadEntity);
            }
        }
        /// g更新操作
        /// </summary>
        /// <param name="uid">用户主键</param>
        /// <param name="oid">关联主键</param>
        /// <param name="category">类型</param>
        /// <param name="operatType"></param>
        public void SetForm(string uid, string oid, string category, OperatType operatType)
        {
            string Uniqueid = "";
            MessageReadEntity messageReadEntity = new MessageReadEntity();
            var expression = LinqExtensions.True<MessageReadEntity>();
            expression = expression.And(t => t.UserId == uid && t.MessageId == oid && t.Category == category);
            var message= this.BaseRepository().FindEntity(expression);
            if (message != null)
            {
                Uniqueid = message.Uniqueid;
                messageReadEntity.ModifyDate = DateTime.Now;
                messageReadEntity.Uniqueid = Uniqueid;
            }
            else
            { 
                messageReadEntity.CreateDate = DateTime.Now;
                messageReadEntity.MessageId = oid;
                messageReadEntity.UserId = uid;
                messageReadEntity.Category = category;
            }
            messageReadEntity.IsRead = true;
            if (operatType == OperatType.AppRead) { messageReadEntity.AppRead = true; }
            if (operatType == OperatType.IsLike) { messageReadEntity.IsLike = true; }
            if (operatType == OperatType.PCRead) { messageReadEntity.PCRead = true; } 

            SaveForm(Uniqueid, messageReadEntity);
        }

        /// g更新操作
        /// </summary>
        /// <param name="uid">用户主键</param>
        /// <param name="oid">关联主键</param>
        /// <param name="category">类型</param>
        /// <param name="operatType"></param>
        public int SignInMark(string uid, string oid, string category, OperatType operatType, string SignInDescription)
        {
            MessageReadEntity messageReadEntity = new MessageReadEntity();
            var expression = LinqExtensions.True<MessageReadEntity>();
            expression = expression.And(t => t.UserId == uid && t.MessageId == oid && t.Category == category);
            var message = this.BaseRepository().FindEntity(expression);
            if (message != null)
            {
                messageReadEntity.Uniqueid = message.Uniqueid;
                messageReadEntity.SignInDescription = SignInDescription;
                if (operatType == OperatType.SignIn) { messageReadEntity.SignInMark = true; }
                if (operatType == OperatType.NoSignIn) { messageReadEntity.SignInMark = false; }
                this.BaseRepository().Update(messageReadEntity);
                return 1;
            }
            else
            {
                return -1;
            } 
        }
        #endregion
    }
}
