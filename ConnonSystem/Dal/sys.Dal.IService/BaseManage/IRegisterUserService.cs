using sys.Dal.Entity.BaseManage;
using sys.Util.WebControl;
using System.Collections.Generic;
using System.Data;
using sys.Aplication.Code;
namespace sys.Dal.IService.BaseManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c)  
    /// 创建人：宋江
    /// 日 期：2015.11.03 10:58
    /// 描 述：用户管理
    /// </summary>
    public interface IRegisterUserService
    {
        #region 获取数据
        /// <summary>
        /// 用户列表
        /// </summary>
        /// <returns></returns>
        DataTable GetTable();
        /// <summary>
        /// 用户列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<RegisterUserEntity> GetList();
        /// <summary>
        /// 根据条件查询用户
        /// </summary>
        /// <returns></returns>
        IEnumerable<RegisterUserEntity> GetUserSearch(RegisterUserEntity user);
        /// <summary>
        /// 用户列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<RegisterUserEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 用户列表（ALL）
        /// </summary>
        /// <returns></returns>
        DataTable GetAllTable();
        /// <summary>
        /// 用户实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        RegisterUserEntity GetEntity(string keyValue);
        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns></returns>
        RegisterUserEntity CheckLogin(string username);
        /// <summary>
        /// 导出用户列表
        /// </summary>
        /// <returns></returns>
        DataTable GetExportList();

        /// <summary>
        /// 查询手机号
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns></returns>
        RegisterUserEntity CheckMobile(string Mobile);
        #endregion

        #region 验证数据
        /// <summary>
        /// 账户不能重复
        /// </summary>
        /// <param name="account">账户值</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        bool ExistAccount(string account, string keyValue);
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="keyValue">主键</param>
        void RemoveForm(string keyValue);
        /// <summary>
        /// 保存用户表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="userEntity">用户实体</param>
        /// <returns></returns>
        string SaveForm(string keyValue, RegisterUserEntity userEntity);
        /// <summary>
        /// 修改用户登录密码
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="Password">新密码（MD5 小写）</param>
        void RevisePassword(string keyValue, string Password);

        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="audit"> 审核结果1，通过，2未通过</param>
        /// <param name="verifyDescribe">备注 </param>
        void SaveReviseAudit(string keyValue, int audit, string verifyDescribe);
        /// <summary>
        /// 修改用户状态
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="State">状态：1-启动；0-禁用</param>
        void UpdateState(string keyValue, int State);
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="userEntity">实体对象</param>
        void UpdateEntity(RegisterUserEntity userEntity);
        #endregion
    }
}

