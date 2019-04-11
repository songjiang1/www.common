using sys.Dal.Busines.BaseManage;
using sys.Dal.Cache;
using sys.Aplication.Code;
using sys.Dal.Entity.BaseManage;
using sys.Util;
using sys.Util.WebControl;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using sys.Application.app.Controllers.Extensions;

namespace sys.Application.app.Controllers
{
    /// <summary>
    /// 版 本 2.0
    /// Copyright (c)  
    /// 创建人：宋江
    /// 日 期：2015.11.02 14:27
    /// 描 述：机构管理
    /// </summary>
    public class OrganizeController : BaseController
    {
        private OrganizeBLL organizeBLL = new OrganizeBLL();
        private OrganizeCache organizeCache = new OrganizeCache();

         

        #region 获取数据
        /// <summary>
        /// 机构列表 
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <returns>返回Json</returns> 
        public ActionResult GetListJson(string OrganizeId, string ParentId)
        {
            var data = organizeCache.GetList().OrderBy(t=>t.SortCode).ToList();
            if (!string.IsNullOrEmpty(OrganizeId))
            {
                data = data.Where(t => t.OrganizeId == OrganizeId).ToList();
            }
            if (!string.IsNullOrEmpty(ParentId))
            {
                var dt = data.Where(t => t.ParentId == ParentId).ToList();
                if(dt.Count == 1)
                    {
                          data = data.Where(t => t.ParentId == ParentId|| t.ParentId == dt[0].OrganizeId).ToList();
                }
                else
                {
                    data = dt;
                }
                
            } 
            return Success(data.ToJson());
        }
        /// <summary>
        /// 机构列表 
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="keyword">关键字</param>
        /// <returns>返回树形列表Json</returns>
       
        public ActionResult GetTreeListJson(string ParentId)
        {
            var data = organizeBLL.GetList().OrderBy(t => t.SortCode).ToList();
            
            var treeList = new List<TreeGridEntity>();
            foreach (OrganizeEntity item in data)
            {
                TreeGridEntity tree = new TreeGridEntity();
                bool hasChildren = data.Count(t => t.ParentId == item.OrganizeId) == 0 ? false : true;
                tree.id = item.OrganizeId;
                tree.hasChildren = hasChildren;
                tree.parentId = item.ParentId;
                tree.expanded = true;
                tree.entityJson = item.ToJson();
                treeList.Add(tree);
            }
            if (!string.IsNullOrEmpty(ParentId))
            {
                treeList = treeList.Where(t => t.id == ParentId||t.parentId== ParentId).ToList();
            }
            //return Success(treeList.TreeJson());
            return Success(treeList.ToJson());
        }
         
        #endregion

         

         
    }
}
