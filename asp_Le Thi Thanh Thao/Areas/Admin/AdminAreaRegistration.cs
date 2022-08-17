using System.Web.Mvc;

namespace asp_Le_Thi_Thanh_Thao.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                 new[] { "asp_Le_Thi_Thanh_Thao.Areas.Admin.Controllers" }
            );
        }
    }
}