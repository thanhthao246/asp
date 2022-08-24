using asp_Le_Thi_Thanh_Thao.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace asp_Le_Thi_Thanh_Thao.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        // GET: Admin/Order
        QL_BanHangEntities3 objQL_BanHangEntities2 = new QL_BanHangEntities3();
        public ActionResult Index()
        {
            var listOrder = objQL_BanHangEntities2.Orders.ToList();
            return View(listOrder);
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            var order = objQL_BanHangEntities2.Orders.Where(n => n.Id == id).FirstOrDefault();
            return View(order);
        }
        [HttpGet]
        public ActionResult Delete(int Id)
        {

            var objorder = objQL_BanHangEntities2.Orders.Where(n => n.Id == Id).FirstOrDefault();

            return View(objorder);
        }
        [HttpPost]
        public ActionResult Delete(Order objor)
        {

            var objorder = objQL_BanHangEntities2.Orders.Where(n => n.Id == objor.Id).FirstOrDefault();
            objQL_BanHangEntities2.Orders.Remove(objorder);
            objQL_BanHangEntities2.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
