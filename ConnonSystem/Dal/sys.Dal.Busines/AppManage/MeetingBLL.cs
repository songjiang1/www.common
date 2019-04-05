using sys.Dal.Entity.AppManage;
using sys.Dal.IService.AppManage;
using sys.Dal.Service.AppManage;
using sys.Util;
using sys.Util.WebControl;
using System;
using System.Collections.Generic;

namespace sys.Dal.Busines.AppManage
{
    /// <summary>
    /// 版 本 2.0
    /// Copyright (c)  
    /// 创建人：宋江
    /// 日 期：2015.12.7 16:40
    /// 描 述：电子会议
    /// </summary>
    public class MeetingBLL
    {
        private IMeetingService service = new MeetingService();

        #region 获取数据
        /// <summary>
        /// 会议列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<MeetingEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        /// <summary>
        /// 会议实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public MeetingEntity GetEntity(string keyValue)
        {
            MeetingEntity meetingEntity = service.GetEntity(keyValue);
            meetingEntity.MeetingContent = WebHelper.HtmlDecode(meetingEntity.MeetingContent);
            return meetingEntity;
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除会议
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
        /// 保存会议表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="newsEntity">会议实体</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, MeetingEntity meetingEntity)
        {
            try
            {
                meetingEntity.MeetingContent = WebHelper.HtmlEncode(meetingEntity.MeetingContent);
                service.SaveForm(keyValue, meetingEntity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 更新浏览量 
        /// </summary>
        /// <param name="keyValue"></param>
        public void PvPlusOne(string keyValue)
        {
            try
            {
                service.PvPlusOne(keyValue);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 保持二维码
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="signQRCode">二维码路径</param>
        public void UpdateSignQRCode(string keyValue,string signQRCode)
        {
            try
            {
                service.UpdateSignQRCode(keyValue, signQRCode);
            }
            catch (Exception)
            {
                throw;
            }
        }

      
        #endregion
    }
}
