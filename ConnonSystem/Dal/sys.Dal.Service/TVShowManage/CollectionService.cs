using sys.Bll;
using sys.Bll.Repository;
using sys.Util;
using sys.Util.WebControl;
using sys.Util.Extension;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Linq; 
using System;
using sys.Aplication.Code;
using sys.Dal.Entity.TVShowManage;
using sys.Dal.IService.TVShowManage;

namespace sys.Dal.Service.TVShowManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c)  
    /// 创建人：宋江
    /// 日 期：2015.11.03 10:58
    /// 描 述：用户管理
    /// </summary>
    public class CollectionService : RepositoryFactory<CollectionEntity>, ICollectionService
    { 

        #region 获取数据
        /// <summary>
        /// 用户列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetTable(CollectionEntity collection, string OrderBy)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"  SELECT  Id,Title,TVShowMenuId,HeadIcon,Initials,Spelling,Mobile,ParentId,SortCode,Position,WorkUnit,ContactAddress
                              FROM    b_tv_collection u   
                              WHERE   u.EnabledMark = 1   ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrWhiteSpace(collection.ParentId))
            {
                strSql.Append("  AND (ParentId=@ParentId OR  Id=@ParentId )");
                parameter.Add(DbParameters.CreateDbParameter("@ParentId", collection.ParentId));
            }
            if (!string.IsNullOrWhiteSpace(collection.TVShowMenuId))
            {
                strSql.Append("  AND  TVShowMenuId=@TVShowMenuId");
                parameter.Add(DbParameters.CreateDbParameter("@TVShowMenuId", collection.TVShowMenuId));
            }
            if (!string.IsNullOrWhiteSpace(collection.OrganizeId))
            {
                strSql.Append("  AND  OrganizeId=@OrganizeId");
                parameter.Add(DbParameters.CreateDbParameter("@OrganizeId", collection.OrganizeId));
            }
            if (!collection.Title.IsEmpty())
            {
                strSql.Append(" AND Title like @Title");
                parameter.Add(DbParameters.CreateDbParameter("@Title", '%' + collection.Title + '%'));
            }
            if (!collection.CreateUserId.IsEmpty())
            {
                strSql.Append(" AND CreateUserId = @CreateUserId");
                parameter.Add(DbParameters.CreateDbParameter("@CreateUserId",collection.CreateUserId));
            }

            if (!OrderBy.IsEmpty())
            {
                strSql.Append(" "+ OrderBy); 
            }
            return this.BaseRepository().FindTable(strSql.ToString(),parameter.ToArray());
        }
        /// <summary>
        /// 用户首字母
        /// </summary>
        /// <returns></returns>
        public DataTable GetInitials(CollectionEntity collection)
        {
            var strSql = new StringBuilder();

            strSql.Append(@"   SELECT  u.Initials,IFNULL(COUNT(1),0) as InitialsCount
                               FROM    b_tv_collection u  
                               WHERE  u.EnabledMark = 1    ");

            var parameter = new List<DbParameter>(); 
            if (!string.IsNullOrWhiteSpace(collection.ParentId))
            {
                strSql.Append("  AND (ParentId=@ParentId OR  Id=@ParentId )");
                parameter.Add(DbParameters.CreateDbParameter("@ParentId", collection.ParentId));
            }
            if (!string.IsNullOrWhiteSpace(collection.TVShowMenuId))
            {
                strSql.Append("  AND  TVShowMenuId=@TVShowMenuId");
                parameter.Add(DbParameters.CreateDbParameter("@TVShowMenuId", collection.TVShowMenuId));
            }
            if (!string.IsNullOrWhiteSpace(collection.OrganizeId))
            {
                strSql.Append("  AND  OrganizeId=@OrganizeId");
                parameter.Add(DbParameters.CreateDbParameter("@OrganizeId", collection.OrganizeId));
            }
            if (!collection.Title.IsEmpty())
            {
                strSql.Append(" AND Title like @Title");
                parameter.Add(DbParameters.CreateDbParameter("@Title", '%' + collection.Title + '%'));
            }
            strSql.Append(@"     GROUP BY u.Initials "); 
            return this.BaseRepository().FindTable(strSql.ToString(), parameter.ToArray());
        }
        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CollectionEntity> GetList()
        {
            var expression = LinqExtensions.True<CollectionEntity>();
            expression = expression.And(t => t.EnabledMark == 1).And(t => t.DeleteMark == 0);
            return this.BaseRepository().IQueryable(expression).OrderByDescending(t => t.CreateDate).ToList();
        }
       
        /// <summary>
        ///  列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<CollectionEntity> GetPageList(Pagination pagination, string queryJson)
        {

            var expression = LinqExtensions.True<CollectionEntity>();
            var queryParam = queryJson.ToJObject();  
            //查询条件
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyord = queryParam["keyword"].ToString();
                switch (condition)
                { 
                    case "Mobile":          //手机
                        expression = expression.And(t => t.Mobile.Contains(keyord));
                        break;
                    default:
                        break;
                }
            }
            return this.BaseRepository().FindList(expression,  pagination); 
        }
        /// <summary>
        /// 用户列表（ALL）
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllTable()
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT  u.Title , 
                                    u.PostName ,
                                    u.EnabledMark ,
                                    u.CreateDate,
                                    u.Description
                            FROM    Base_User u
                                    LEFT JOIN Base_Organize o ON o.OrganizeId = u.OrganizeId
                                    LEFT JOIN Base_Department d ON d.DepartmentId = u.DepartmentId
                            WHERE   1=1");
            strSql.Append(" AND   u.EnabledMark = 1 AND u.DeleteMark=0 order by o.FullName,d.FullName,u.RealName");
            return this.BaseRepository().FindTable(strSql.ToString());
        }
        /// <summary>
        /// 用户列表（导出Excel）
        /// </summary>
        /// <returns></returns>
        public DataTable GetExportList()
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT [Account]
                                  ,[RealName]
                                  ,CASE WHEN Gender=1 THEN '男' ELSE '女' END AS Gender
                                  ,[Birthday]
                                  ,[Mobile]
                                  ,[Telephone]
                                  ,u.[Email]
                                  ,[WeChat]
                                  ,[MSN]
                                  ,u.[Manager]
                                  ,o.FullName AS Organize
                                  ,d.FullName AS Department
                                  ,u.[Description]
                                  ,u.[CreateDate]
                                  ,u.[CreateUserName]
                              FROM Base_User u
                              INNER JOIN Base_Department d ON u.DepartmentId=d.DepartmentId
                              INNER JOIN Base_Organize o ON u.OrganizeId=o.OrganizeId");
            return this.BaseRepository().FindTable(strSql.ToString());
        }
        /// <summary>
        /// 用户实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public CollectionEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        /// <summary>
        /// 根据条件查询
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IEnumerable<CollectionEntity> GetUserSearch(CollectionEntity user)
        {
            var expression = LinqExtensions.True<CollectionEntity>(); 
            expression = expression.And(t => t.EnabledMark == 1).And(t => t.Title != "System");
            if (!string.IsNullOrWhiteSpace(user.OpenId))
            {
                expression = expression.And(t => t.OpenId == user.OpenId);
            }
             
            return this.BaseRepository().IQueryable(expression).OrderByDescending(t => t.CreateDate);  
        }
     

        /// <summary>
        /// 查询手机号
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns></returns>
        public CollectionEntity CheckMobile(string Mobile)
        {
            var expression = LinqExtensions.True<CollectionEntity>();
            expression = expression.And(t => t.EnabledMark == 1);
            expression = expression.And(t => t.Mobile == Mobile); 
            return this.BaseRepository().FindEntity(expression);
        }
        #endregion

         

        #region 提交数据
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// 保存用户表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="userEntity">用户实体</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, CollectionEntity collection)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                collection.Modify(keyValue);
                this.BaseRepository().Update(collection);
            }
            else
            {

                collection.Create();
                this.BaseRepository().Insert(collection);
            }
        }
         
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="userEntity">实体对象</param>
        public void UpdateEntity(CollectionEntity userEntity)
        {
            this.BaseRepository().Update(userEntity);
        }
        #endregion
    }
}
