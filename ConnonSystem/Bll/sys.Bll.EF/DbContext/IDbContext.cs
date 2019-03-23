using System;
using System.Data.Entity.Infrastructure;

namespace sys.Bll.EF
{
    /// <summary>
    /// 版 本 2.0
    /// Copyright (c)  
    /// 创建人：宋江
    /// 日 期：2016.04.07
    /// 描 述：数据库连接接口 
    /// </summary>
    public interface IDbContext: IDisposable, IObjectContextAdapter
    {
    }
}
