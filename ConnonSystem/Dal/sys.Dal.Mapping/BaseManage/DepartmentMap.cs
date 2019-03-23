
using sys.Dal.BaseManage;
using sys.Dal.Entity.BaseManage;
using System.Data.Entity.ModelConfiguration;

namespace sys.Dal.Mapping.BaseManage
{
    /// <summary>
    /// 版 本 2.0
    /// Copyright (c)  
    /// 创建人：宋江
    /// 日 期：2015.11.02 14:27
    /// 描 述：部门管理
    /// </summary>
    public class DepartmentMap : EntityTypeConfiguration<DepartmentEntity>
    {
        public DepartmentMap()
        {
            #region 表、主键
            //表
            this.ToTable("Base_Department");
            //主键
            this.HasKey(t => t.DepartmentId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
