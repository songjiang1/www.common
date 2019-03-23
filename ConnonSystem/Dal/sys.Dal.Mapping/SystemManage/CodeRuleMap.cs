
using sys.Dal.Entity.SystemManage;
using System.Data.Entity.ModelConfiguration;

namespace sys.Dal.Mapping.SystemManage
{
    /// <summary>
    /// 版 本 2.0
    /// Copyright (c)  
    /// 创建人：宋江
    /// 日 期：2015.12.21 16:19
    /// 描 述：编号规则
    /// </summary>
    public class CodeRuleMap : EntityTypeConfiguration<CodeRuleEntity>
    {
        public CodeRuleMap()
        {
            #region 表、主键
            //表
            this.ToTable("Base_CodeRule");
            //主键
            this.HasKey(t => t.RuleId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
