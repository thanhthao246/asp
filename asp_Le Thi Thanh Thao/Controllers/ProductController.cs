using asp_Le_Thi_Thanh_Thao.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace asp_Le_Thi_Thanh_Thao.Controllers
{
    public class ProductController : Controller
    {
        QL_BanHangEntities1 objQL_BanHangEntities1 = new QL_BanHangEntities1();
        // GET: Product
        public ActionResult Detail(int Id)
        {
            var objProduct = objQL_BanHangEntities1.Products.Where(n => n.Id == Id).FirstOrDefault();
            return View(objProduct);
        }
    }
}
