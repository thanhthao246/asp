using System;
using System.ComponentModel.DataAnnotations;

namespace asp_Le_Thi_Thanh_Thao.Models
{
    public partial class CategoryMasterData
    {
        public int Id { get; set; }
        [Display(Name = "Tên danh mục")]
        [Required(ErrorMessage = "Tên loại danh mục không được để trống")]
        public string Name { get; set; }
        [Display(Name = "Hình ảnh")]
        public string Avatar { get; set; }
        [Display(Name = "Độ phổ biến")]
        public Nullable<int> IsPopular { get; set; }
        public string Slug { get; set; }
        [Display(Name = "Hiển thị lên trang chủ")]
        public Nullable<bool> ShowOnHomePage { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
        public Nullable<bool> Deleted { get; set; }
        [Display(Name = "Ngày tạo")]
        public Nullable<System.DateTime> CreatedOnUtc { get; set; }
        [Display(Name = "Ngày cập nhật")]
        public Nullable<System.DateTime> UpdatedOnUtc { get; set; }

    }
}