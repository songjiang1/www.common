using sys.Dal.Entity.AppManage;
using sys.Dal.IService.AppManage;
using sys.Dal.Service.AppManage;
using sys.Util;
using sys.Util.WebControl;
using System;
using System.Data;
using System.Collections.Generic;

namespace sys.Dal.Busines.AppManage
{
    /// <summary>
    /// 版 本 2.0
    /// Copyright (c)  
    /// 创建人：宋江
    /// 日 期：2015.12.7 16:40
    /// 描 述：电子公告
    /// </summary>
    public class NoticeBLL
    {
        private INoticeService service = new NoticeService();

        #region 获取数据
        /// <summary>
        /// 公告列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<NoticeEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        /// <summary>
        /// 公告实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public NoticeEntity GetEntity(string keyValue)
        {
            NoticeEntity noticeEntity = service.GetEntity(keyValue);
            noticeEntity.NewsContent = WebHelper.HtmlDecode(noticeEntity.NewsContent);
            return noticeEntity;
        }
        /// <summary>
        /// 获取数据 app段展示
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public DataTable GetEntity(Pagination pagination)
        {
            return service.GetTable(pagination);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除公告
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
        /// 保存公告表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="newsEntity">公告实体</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, NoticeEntity newsEntity)
        {
            try
            {
                newsEntity.NewsContent = WebHelper.HtmlEncode(newsEntity.NewsContent);
                service.SaveForm(keyValue, newsEntity);
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
        #endregion
    }
}
