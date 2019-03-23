using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using sys.Util;
using System;
using System.Configuration;
namespace sys.Bll.Repository
{
    /// <summary>
    /// 版 本 2.0
    /// Copyright (c)  
    /// 创建人：宋江
    /// 日 期：2015.10.10
    /// 描 述：定义仓储模型工厂
    /// </summary>
    public class RepositoryFactory
    {
        /// <summary>
        /// 定义仓储
        /// </summary>
        /// <param name="connString">连接字符串</param>
        /// <returns></returns>
        public IRepository BaseRepository(string connString)
        {
            DatabaseType  s=(DatabaseType)Enum.Parse(typeof(DatabaseType), Config.GetValue("connDataType")); 


            return new Repository(DbFactory.Base(connString, DatabaseType.MySql));
        }
        /// <summary>
        /// 定义仓储（基础库）
        /// </summary>
        /// <returns></returns>
        public IRepository BaseRepository()
        {
            return new Repository(DbFactory.Base());
        }
    }
}
