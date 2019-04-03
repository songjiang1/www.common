using sys.Dal.Entity;
using sys.Dal.Entity.AppManage;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sys.Dal.Mapping
{
  public class NotifyMap : EntityTypeConfiguration<NotifyEntity>
    { 
        public NotifyMap()
        {
            #region 表、主键
            //表
            this.ToTable("b_notify");
            //主键
            this.HasKey(t => t.NotifyId);
            #endregion

            #region 配置关系
            #endregion
        } 
    }
}
