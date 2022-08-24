using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace asp_Le_Thi_Thanh_Thao.Models
{
    public class OrderMasterData
    {
        public int Id { get; set; }
        [Display(Name = "Tên")]
        public string Name { get; set; }
        [Display(Name = "Tài khoản")]
        public int UserId { get; set; }
        [Display(Name = "Trạng thái")]
        public int Status { get; set; }
        public System.DateTime CreatedOnUtc { get; set; }
    }
}