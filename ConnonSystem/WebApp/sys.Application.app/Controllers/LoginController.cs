using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sys.Aplication.Code;
using sys.Application.app.Controllers.Extensions;
using sys.Dal.Busines.AuthorizeManage;
using sys.Dal.Busines.BaseManage;
using sys.Dal.Busines.SystemManage;
using sys.Dal.Entity.BaseManage;
using sys.Dal.Entity.SystemManage;
using sys.Util;
using sys.Util.Attributes;
using sys.Util.WebControl;

namespace sys.Application.app.Controllers
{
    public class LoginController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public ActionResult CheckLogin(string username, string password)
        {
            LogEntity logEntity = new LogEntity();


            logEntity.CategoryId = 1;
            logEntity.OperateTypeId = ((int)OperationType.Login).ToString();
            logEntity.OperateType = EnumAttribute.GetDescription(OperationType.Login);
            logEntity.OperateAccount = username;
            logEntity.OperateUserId = username;
            logEntity.Module = Config.GetValue("SoftName");

            try
            { 
                 
                #region 账户验证
                RegisterUserEntity userEntity = new RegisterUserBLL().CheckLogin(username, password);
                if (userEntity != null)
                {
                    AuthorizeBLL authorizeBLL = new AuthorizeBLL();
                    Operator operators = new Operator();
                    operators.UserId = userEntity.UserId;
                    operators.Code = userEntity.EnCode;
                    operators.Account = userEntity.Account;
                    operators.UserName = userEntity.RealName;
                    operators.Password = userEntity.Password;
                    operators.Secretkey = userEntity.Secretkey;
                    operators.CompanyId = userEntity.OrganizeId;
                    operators.DepartmentId = userEntity.DepartmentId;
                    operators.IPAddress = Net.Ip;
                    operators.IPAddressName = IPLocation.GetLocation(Net.Ip);
                    operators.HeadIcon = userEntity.HeadIcon;
                    operators.Post = userEntity.Post;
                    operators.Position = userEntity.Position;
                    operators.WorkUnit = userEntity.WorkUnit;
                    //operators.ObjectId = new PermissionBLL().GetObjectStr(userEntity.UserId);
                    operators.LogTime = DateTime.Now;
                    operators.Token = DESEncrypt.Encrypt(Guid.NewGuid().ToString()); 
                    //判断是否系统管理员
                    if (userEntity.Account == "System")
                    {
                        operators.IsSystem = true;
                    }
                    else
                    {
                        operators.IsSystem = false;
                    }
                    OperatorProvider.Provider.AddCurrent(operators);
                    //登录限制
                    //LoginLimit(username, operators.IPAddress, operators.IPAddressName);
                    //写入日志
                    logEntity.ExecuteResult = 1;
                    logEntity.ExecuteResultJson = "登录成功";
                    logEntity.WriteLog();
                }
                return Content(new AjaxResult { type = ResultType.success, message = "登录成功" }.ToJson());
                #endregion
            }
            catch (Exception ex)
            {
                WebHelper.RemoveCookie("sys_autologin");                  //清除自动登录
                logEntity.ExecuteResult = -1;
                logEntity.ExecuteResultJson = ex.Message;
                logEntity.WriteLog(); 
                return Content(new AjaxResult { type = ResultType.error, message = ex.Message }.ToJson());
            }
        }

    }
}