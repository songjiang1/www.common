 
using sys.Cache.Factory;
using sys.Util;
using sys.Util.Extension;
using sys.Util.Offices;
using sys.Util.SignalR;
using sys.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using sys.Aplication.Code;
using sys.Dal.Service.TVShowManage;
using sys.Dal.IService.TVShowManage;
using sys.Dal.Entity.TVShowManage;

namespace sys.Dal.Busines.TVShowManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c)  
    /// 创建人：宋江
    /// 日 期：2015.11.03 10:58
    /// 描 述：用户管理
    /// </summary>
    public class CollectionBLL
    {
        private ICollectionService service = new CollectionService(); 

        #region 获取数据
        /// <summary>
        /// 用户列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetTable(CollectionEntity collection, string OrderBy)
        {
            return service.GetTable(collection,  OrderBy);
        }
        /// <summary>
        /// 用户首字母
        /// </summary>
        /// <returns></returns>
        public DataTable GetInitials(CollectionEntity collection)
        {
            return service.GetInitials(collection);
        }
        /// <summary>
        /// 用户列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CollectionEntity> GetList()
        {
            return service.GetList();
        }
        /// <summary>
        /// 根据条件查询用户
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CollectionEntity> GetUserSearch(CollectionEntity collection)
        {
            return service.GetUserSearch(collection);
        }
        /// <summary>
        /// 用户列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<CollectionEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        /// <summary>
        /// 用户列表（ALL）
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllTable()
        {
            return service.GetAllTable();
        }
        /// <summary>
        /// 用户实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public CollectionEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        /// <summary>
        /// 查询手机号
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns></returns>
        public CollectionEntity CheckMobile(string Mobile)
        { 
            return service.CheckMobile(Mobile);
        }
        #endregion

         

        #region 提交数据
        /// <summary>
        /// 删除用户
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
        /// 保存用户表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="userEntity">用户实体</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, CollectionEntity collection)
        {
            try
            {
                if(string.IsNullOrWhiteSpace(collection.SimpleSpelling))
                {
                    if (Str.CheckStringChineseReg(collection.Title))
                    {
                        collection.SimpleSpelling = Str.PinYin(collection.Title).ToUpper();
                        collection.Initials = collection.SimpleSpelling.Substring(0,1);
                    }
                    else
                    {
                        collection.SimpleSpelling = collection.Title.ToUpper();
                        collection.Initials = collection.SimpleSpelling.Substring(0, 1);
                    }
                } 
               service.SaveForm(keyValue, collection);  
            }
            catch (Exception)
            {
                throw;
            }
        }
         
        #endregion

        #region 处理数据
        /// <summary>
        /// 导出用户列表
        /// </summary>
        /// <returns></returns>
        public void GetExportList()
        {
            //取出数据源
            DataTable exportTable = service.GetExportList();
            //设置导出格式
            ExcelConfig excelconfig = new ExcelConfig();
            excelconfig.Title = "测试用户导出";
            excelconfig.TitleFont = "微软雅黑";
            excelconfig.TitlePoint = 25;
            excelconfig.FileName = "用户导出.xls";
            excelconfig.IsAllSizeColumn = true;
            //每一列的设置,没有设置的列信息，系统将按datatable中的列名导出
            List<ColumnEntity> listColumnEntity = new List<ColumnEntity>();
            excelconfig.ColumnEntity = listColumnEntity;
            ColumnEntity columnentity = new ColumnEntity();
            excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "account", ExcelColumn = "账户" });
            excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "realname", ExcelColumn = "姓名" });
            excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "gender", ExcelColumn = "性别" });
            excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "birthday", ExcelColumn = "生日" });
            excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "mobile", ExcelColumn = "手机", Background = Color.Red });
            excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "telephone", ExcelColumn = "电话", Background = Color.Red });
            excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "wechat", ExcelColumn = "微信" });
            excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "manager", ExcelColumn = "主管" });
            excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "organize", ExcelColumn = "公司" });
            excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "department", ExcelColumn = "部门" });
            excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "description", ExcelColumn = "说明" });
            excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "createdate", ExcelColumn = "创建日期" });
            excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "createusername", ExcelColumn = "创建人" });
            //调用导出方法
            ExcelHelper.ExcelDownload(exportTable, excelconfig);
            //从泛型Lis导出
            //TExcelHelper<DepartmentEntity>.ExcelDownload(department.GetList().ToList(), excelconfig);
        }
        #endregion
    }
}
