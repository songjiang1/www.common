using sys.Dal.Entity.AppManage;
using sys.Dal.IService.AppManage;
using sys.Bll.Repository;
using sys.Util;
using sys.Util.Extension;
using sys.Util.WebControl;
using System.Collections.Generic;
using System;
using sys.Aplication.Code;

namespace sys.Dal.Service.AppManage
{
    /// <summary>
    /// 版 本 2.0
    /// Copyright (c)  
    /// 创建人：宋江
    /// 日 期：2015.12.7 16:40
    /// 描 述：电子会议
    /// </summary>
    public class MeetingService : RepositoryFactory<MeetingEntity>, IMeetingService
    {
        private IMessageReadService messageReadService = new MessageReadService();
        #region 获取数据
        /// <summary>
        /// 会议列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<MeetingEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<MeetingEntity>();
            var newTime = DateTime.Now;
            var queryParam = queryJson.ToJObject();
            if (!queryParam["FullHead"].IsEmpty())
            {
                string FullHead = queryParam["FullHead"].ToString();
                expression = expression.And(t => t.FullHead.Contains(FullHead));
            }
            if (!queryParam["State"].IsEmpty())
            {
                if (queryParam["State"].ToString().Trim() == "0")
                {
                    expression = expression.And(t => t.ConveneETime >= newTime); 
                }
                else
                {
                    expression = expression.And(t => t.ConveneETime < newTime); 
                }  
            }
            
            return this.BaseRepository().FindList(expression, pagination);
        }
        /// <summary>
        /// 会议实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public MeetingEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除会议
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// 保存会议表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="newsEntity">会议实体</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, MeetingEntity meetingEntity)
        {
            
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                #region MyRegion 基本信息
                if (!string.IsNullOrEmpty(keyValue))
                {
                    meetingEntity.Modify(keyValue);
                    meetingEntity.ObjectName = string.IsNullOrWhiteSpace(meetingEntity.ObjectName) ? "" : meetingEntity.ObjectName;
                    db.Update(meetingEntity); 
                }
                else
                {
                    meetingEntity.Create(); 
                    db.Insert(meetingEntity);
                }
                #endregion

                #region MyRegion 发送会议通知
                db.Delete<MessageReadEntity>(t => t.MessageId == keyValue && t.Category == "1" &&  t.IsRead==false && t.IsLike == false && t.AppRead == false && t.PCRead == false && t.SignInMark == false);
                List<MessageReadEntity> messageReadEntityList = new List<MessageReadEntity>();
                var userString = meetingEntity.ObjectName;
                if (!string.IsNullOrEmpty(userString))
                {
                    var userlist = userString.Split('|');
                    for (int i = 0; i < userlist.Length; i++)
                    {
                        var userid = userlist[i].Split(',')[0];
                        if (messageReadService.ExistUser(keyValue, userid, "1", ""))
                        {
                            messageReadEntityList.Add(new MessageReadEntity
                            {
                                Uniqueid=Guid.NewGuid().ToString(),
                                Category = "1",//会议
                                MessageId = meetingEntity.MeetingId,
                                UserId = userlist[i].Split(',')[0],
                                CreateDate = DateTime.Now,
                                CreateUserId = OperatorProvider.Provider.Current().UserId,
                                CreateUserName = OperatorProvider.Provider.Current().UserName
                            });
                             
                        }
                       
                           
                    }
                    db.Insert(messageReadEntityList); 
                }

                #endregion
                db.Commit(); 
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }

        /// <summary>
        /// 更新浏览量
        /// </summary>
        /// <param name="keyValue"></param>
        public void PvPlusOne(string keyValue)
        {
            MeetingEntity noticeEntity = new MeetingEntity();
            noticeEntity.MeetingId = keyValue;
            noticeEntity.PV = this.BaseRepository().FindEntity(keyValue).PV + 1;
            this.BaseRepository().Update(noticeEntity);
        }

        /// <summary>
        /// 保持二维码
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="signQRCode">二维码路径</param>
        public void UpdateSignQRCode(string keyValue,string signQRCode)
        {
            MeetingEntity noticeEntity = new MeetingEntity();
            noticeEntity.MeetingId = keyValue;
            noticeEntity.SignQRCode = signQRCode;
            this.BaseRepository().Update(noticeEntity);
        }
        #endregion
    }
}
