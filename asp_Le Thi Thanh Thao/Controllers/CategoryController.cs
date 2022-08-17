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
        QL_BanHangEntities1 objQL_BanHangEntities1 = new QL_BanHangEntities1();
        // GET: Category
        public ActionResult Index()
        {
            var lstCategory = objQL_BanHangEntities1.Categories.ToList();
            return View(lstCategory);
        }
        public ActionResult ProductCategory(int Id)
        {
            var ListProduct = objQL_BanHangEntities1.Products.Where(n => n.CategoryId == Id).ToList();
            return View(ListProduct);
        }
    }
}