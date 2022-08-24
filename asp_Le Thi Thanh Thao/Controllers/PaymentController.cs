using asp_Le_Thi_Thanh_Thao.Context;
using asp_Le_Thi_Thanh_Thao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace asp_Le_Thi_Thanh_Thao.Controllers
{
    public class PaymentController : Controller
    {
        QL_BanHangEntities3 objQL_BanHangEntities2 = new QL_BanHangEntities3();

        // GET: Payment
        public ActionResult Index()
        {
            if (Session["idUser"]==null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                //lay thong tin tu gio hang
                var lstCart = (List<CartModel>)Session["cart"];
                //gan du lieu cho Order
                Order objOrder = new Order();
                objOrder.Name = "DonHang-" + DateTime.Now.ToString("ddMMyyyyHHmmss");
                objOrder.UserId = int.Parse(Session["idUser"].ToString());
                objOrder.CreatedOnUtc = DateTime.Now;
                objOrder.Status = 1;
                objQL_BanHangEntities2.Orders.Add(objOrder);
                //luu vao bang Order
                objQL_BanHangEntities2.SaveChanges();
                //Lay OrderId vua tao luu vao bang OrderDetail
                int intOrderId = objOrder.Id;

                List<OrderDetail> lstOrderDetail = new List<OrderDetail>();
                foreach (var item in lstCart)
                {
                    OrderDetail obj = new OrderDetail();
                    obj.Quantity = item.Quantity;
                    obj.OrderId = intOrderId;
                    obj.ProductId = item.Product.Id;
                    lstOrderDetail.Add(obj);
                }
                objQL_BanHangEntities2.OrderDetails.AddRange(lstOrderDetail);
                objQL_BanHangEntities2.SaveChanges();
            }
            return View();
        }
    }
}
