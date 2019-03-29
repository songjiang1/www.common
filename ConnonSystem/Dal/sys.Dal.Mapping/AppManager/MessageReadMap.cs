using sys.Dal.Entity.AppManager;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sys.Dal.Mapping.AppManager
{
  public class MessageReadMap : EntityTypeConfiguration<MessageReadEntity>
    { 
        public MessageReadMap()
        {
            #region 表、主键
            //表
            this.ToTable("b_message_read");
            //主键
            this.HasKey(t => t.Uniqueid);
            #endregion

            #region 配置关系
            #endregion
        } 
    }
}
