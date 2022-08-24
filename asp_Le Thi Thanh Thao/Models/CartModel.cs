using asp_Le_Thi_Thanh_Thao.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace asp_Le_Thi_Thanh_Thao.Models
{
    public class CartModel
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}