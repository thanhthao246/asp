using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace asp_Le_Thi_Thanh_Thao.Models
{
    public class UserMasterData
    {
        
        public int Id { get; set; }
        [Display(Name = "Họ")]
        [Required(ErrorMessage = "Họ không được để trống")]
        public string LastName { get; set; }
        [Display(Name = "Tên")]
        [Required(ErrorMessage = "Tên không được để trống")]
        public string FirstName { get; set; }
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email không được để trống")]
        public string Email { get; set; }
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password không được để trống")]
        public string Password { get; set; }
        public Nullable<bool> IsAdmin { get; set; }
    }
}