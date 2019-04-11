using sys.Bll.Repository;
using sys.Dal.Entity;
using sys.Dal.IService;
using sys.Util;
using sys.Util.Extension;
using sys.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sys.Dal.Service
{
   public class NotifyService :RepositoryFactory<NotifyEntity>, INotifyService
    {
        #region MyRegion 获取数据
        /// <summary>
        /// 验证信息列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<NotifyEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<NotifyEntity>();
            var queryParam = queryJson.ToJObject();
            if (!queryParam["Type"].IsEmpty())
            {
                string Type = queryParam["Type"].ToString();
                expression = expression.And(t => t.Type.Contains(Type));
            }
            if (!queryParam["Mobile"].IsEmpty())
            {
                string Mobile = queryParam["Mobile"].ToString();
                expression = expression.And(t => t.Mobile == Mobile);
            }
            return this.BaseRepository().FindList(expression, pagination);
        }
        /// <summary>
        /// 验证/通知消息实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public NotifyEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        
        /// <summary>
        /// 检查短信验证码
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool CheckNotify(string mobile,string code)
        {
            var expression = LinqExtensions.True<NotifyEntity>();
            expression = expression.And(t => t.Mobile == mobile);
            //expression = expression.And(t => t.Code == code);
            //expression = expression.And(t => t.Status ==true);
            var NotifyData= this.BaseRepository().IQueryable(expression).OrderByDescending(t => t.CreateDate).FirstOrDefault();
            if (NotifyData == null)
            {
                throw new Exception("手机号错误");
            }
            if (NotifyData.Code != code)
            {
                throw new Exception("验证码错误。");
            }
            if (NotifyData.ExpiresDate <= DateTime.Now)
            {
                throw new Exception("验证码已过期。");
            }
           
            return true;
        }
        #endregion
        #region MyRegion 提交数据
        /// <summary>
        /// 提交数据
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="notify"></param>
        public void SaveForm(string keyValue, NotifyEntity notify)
        {

            if (!string.IsNullOrEmpty(keyValue))
            {
                notify.Modify(keyValue);
                this.BaseRepository().Update(notify);
            }
            else
            {
                //var expression = LinqExtensions.True<NotifyEntity>();
                //expression = expression.And(t => t.Mobile == notify.Mobile);
                //expression = expression.And(t => t.Status == true);
                //var NotifyData = this.BaseRepository().IQueryable(expression).OrderByDescending(t => t.CreateDate).FirstOrDefault();
                //if (NotifyData != null)
                //{
                //    if (NotifyData.ExpiresDate > DateTime.Now)
                //    {
                //        throw new Exception("操作太频繁");
                //    }
                //} 
                notify.Create();
                this.BaseRepository().Insert(notify);
            }
        }
        /// <summary>
        /// 更新短信状态
        /// </summary>
        /// <param name="mobile"></param>
        public bool UpdateNotify(string mobile, string Code)
        {
            var expression = LinqExtensions.True<NotifyEntity>();
            expression = expression.And(t => t.Mobile == mobile);
            expression = expression.And(t => t.Code == Code); 
            var NotifyData = this.BaseRepository().IQueryable(expression).OrderByDescending(t => t.CreateDate).FirstOrDefault();

            NotifyData.Status = true;
           return new RepositoryFactory().BaseRepository().Update(NotifyData)>0?true:false;
        }
        #endregion
    }
}
