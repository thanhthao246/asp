using asp_Le_Thi_Thanh_Thao.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace asp_Le_Thi_Thanh_Thao.Controllers
{
    public class CategoryController : Controller
    {
        QL_BanHangEntities3 objQL_BanHangEntities2 = new QL_BanHangEntities3();
        // GET: Category
        public ActionResult Index()
        {
            var lstCategory = objQL_BanHangEntities2.Categories.ToList();
            return View(lstCategory);
        }
        public ActionResult ProductCategory(int Id)
        {
            var ListProduct = objQL_BanHangEntities2.Products.Where(n => n.CategoryId == Id).ToList();
            return View(ListProduct);
        }
    }
}