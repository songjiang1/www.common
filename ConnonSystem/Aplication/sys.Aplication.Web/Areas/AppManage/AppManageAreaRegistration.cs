using System.Web.Mvc;

namespace sys.Application.Web.Areas.AppManage
{
    public class AppManageAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "AppManage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
              this.AreaName + "_Default",
              this.AreaName + "/{controller}/{action}/{id}",
              new { area = this.AreaName, controller = "Home", action = "Index", id = UrlParameter.Optional },
              new string[] { "sys.Application.Web.Areas." + this.AreaName + ".Controllers" }
            );
        }
    }
}
