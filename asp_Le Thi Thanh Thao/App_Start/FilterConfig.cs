using System.Web;
using System.Web.Mvc;

namespace asp_Le_Thi_Thanh_Thao
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
