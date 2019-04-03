using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sys.Aplication.Code;
using sys.Application.app.Controllers.Extensions;
using sys.Dal.Busines;
using sys.Dal.Busines.AuthorizeManage;
using sys.Dal.Busines.BaseManage;
using sys.Dal.Busines.SystemManage;
using sys.Dal.Entity;
using sys.Dal.Entity.BaseManage;
using sys.Dal.Entity.SystemManage;
using sys.Util;
using sys.Util.Attributes;
using System.Web.Mvc;
using sys.Util.WebControl;

namespace sys.Application.app.Controllers
{
    public class RegistController : Controller
    {
        private RegisterUserBLL registerUserBLL = new RegisterUserBLL();
        private UserBLL userBLL = new UserBLL();
        private NotifyBLL notifyBLL = new NotifyBLL();

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Audit(string Mobile)
        {
            RegisterUserEntity userEntity = new RegisterUserEntity();
            Mobile = Session["Mobile"] == null ? Mobile : Session["Mobile"].ToString();
            if (!string.IsNullOrEmpty(Mobile))
            {
                userEntity = registerUserBLL.CheckMobile(Mobile);
            }
            ViewBag.userEntity = userEntity;
            return View(userEntity);
        }




        /// <summary>
        /// 保存用户表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="userEntity">用户实体</param>
        /// <returns></returns>
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string strUserEntity, string verify)
        {
            try
            {
                RegisterUserEntity userEntity = strUserEntity.ToObject<RegisterUserEntity>();
                var user = userBLL.CheckMobile(userEntity.Mobile);
                if (user != null)
                {
                    userEntity.VerifyMark = 1;
                }
                else
                {
                    userEntity.VerifyMark = 0;
                }
                string keyValue = "";
                if (notifyBLL.CheckNotify(userEntity.Mobile, verify))
                {
                    string objectId = registerUserBLL.SaveForm(keyValue, userEntity);
                }
                //RedirectToAction("/Regist/Audit", new { Mobile = userEntity.Mobile });
                Session["Mobile"] = userEntity.Mobile;
                return Content(new AjaxResult { type = ResultType.success, message = "注册成功" }.ToJson());

            }
            catch (Exception ex)
            {
                return Content(new AjaxResult { type = ResultType.error, message = ex.Message }.ToJson());
            }

        }



        public void SendVerify(string Mobile)
        {
            try
            {
                if (!ValidateUtil.IsMobile(Mobile))
                {
                    throw new Exception("请输入正确的手机号");
                }
                var time = DateTime.Now;
                Random rad = new Random();
                int code = rad.Next(1000, 1000000);
                NotifyEntity notifyEntity = new NotifyEntity();

                notifyEntity.Mobile = Mobile;
                notifyEntity.NotifyId = Guid.NewGuid().ToString();
                notifyEntity.Code = code.ToString();
                notifyEntity.CreateDate = time;
                notifyEntity.ExpiresDate = time.AddMinutes(int.Parse(Config.GetValue("VerifyExpiration")));
                notifyEntity.Type = "1";
                notifyEntity.Status = false;
                notifyBLL.SaveForm("", notifyEntity);

                var result = CCPSMSNotifyUtil.TestNotify(Mobile, code.ToString(), Config.GetValue("VerifyExpiration"));
                if (result == 0)
                {
                    notifyEntity.Status = true;
                    notifyBLL.UpdateNotify(Mobile, notifyEntity.Code);
                }

            }
            catch (Exception ex)
            {

                //return Error(ex.Message);
            }
            //ReturnResult rst = CCPSMSNotifyUtil.TestNotify("18708508278", "2222", "3");
            //ReturnResult rst1 = CCPSMSNotifyUtil.TestNotify("15208547251","2222","3");

        }

    }
}