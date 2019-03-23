
using sys.Dal.Entity.BaseManage;
using System.Data.Entity.ModelConfiguration;

namespace sys.Dal.Mapping.BaseManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c)  
    /// 创建人：宋江
    /// 日 期：2015.11.03 10:58
    /// 描 述：用户管理
    /// </summary>
    public class UserMap : EntityTypeConfiguration<UserEntity>
    {
        public UserMap()
        {
            #region 表、主键
            //表
            this.ToTable("Base_user");
            //主键
            this.HasKey(t => t.UserId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
