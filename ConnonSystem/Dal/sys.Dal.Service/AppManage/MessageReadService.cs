using sys.Dal.Entity.AppManage;
using sys.Dal.IService.AppManage;
using sys.Bll.Repository;
using sys.Util.Extension;
using System.Collections.Generic;
using System.Linq;

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
            return this.BaseRepository().IQueryable(expression).ToList();
        }
        /// <summary>
        /// 分类实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public MessageReadEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
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
            expression = expression.And(t => t.MessageId == messageId&&t.UserId==userId&&t.Category== type);
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
                this.BaseRepository().Insert(messageReadEntity);
            }
        }
        #endregion
    }
}
