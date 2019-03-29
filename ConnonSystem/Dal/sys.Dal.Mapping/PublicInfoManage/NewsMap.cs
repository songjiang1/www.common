using sys.Dal.Entity.PublicInfoManage;
using System.Data.Entity.ModelConfiguration;

namespace sys.Dal.Mapping.PublicInfoManage
{
    /// <summary>
    /// 版 本 2.0
    /// Copyright (c)  
    /// 创建人：宋江
    /// 日 期：2015.12.7 16:40
    /// 描 述：新闻中心
    /// </summary>
    public class NewsMap : EntityTypeConfiguration<NewsEntity>
    {
        public NewsMap()
        {
            #region 表、主键
            //表
            this.ToTable("Base_News");
            //主键
            this.HasKey(t => t.NewsId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
