
using sys.Dal.Entity.SystemManage;
using System.Data.Entity.ModelConfiguration;

namespace sys.Dal.Mapping.SystemManage
{
    /// <summary>
    /// 版 本 2.0
    /// Copyright (c)  
    /// 创建人：宋江
    /// 日 期：2016.1.8 9:56
    /// 描 述：系统日志
    /// </summary>
    public class LogMap : EntityTypeConfiguration<LogEntity>
    {
        public LogMap()
        {
            #region 表、主键
            //表
            this.ToTable("Base_log");
            //主键
            this.HasKey(t => t.LogId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
