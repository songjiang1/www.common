using sys.Dal.Entity.AppManage;
using sys.Util.WebControl;
using System.Collections.Generic;

namespace sys.Dal.IService.AppManage
{
    /// <summary>
    /// 版 本 2.0
    /// Copyright (c)  
    /// 创建人：宋江
    /// 日 期：2015.12.7 16:40
    /// 描 述：会议
    /// </summary>
    public interface IMeetingService
    {
        #region 获取数据
        /// <summary>
        /// 会议列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<MeetingEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 会议实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        MeetingEntity GetEntity(string keyValue);
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除会议
        /// </summary>
        /// <param name="keyValue">主键</param>
        void RemoveForm(string keyValue);
        /// <summary>
        /// 保存会议表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="newsEntity">会议实体</param>
        /// <returns></returns>
        void SaveForm(string keyValue, MeetingEntity meetingEntity);

        /// <summary>
        /// 更新浏览量
        /// </summary>
        /// <param name="keyValue"></param>
        void PvPlusOne(string keyValue);

        /// <summary>
        /// 保持二维码
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="signQRCode">二维码路径</param>
        void UpdateSignQRCode(string keyValue,string signQRCode);

        
        #endregion
    }
}
