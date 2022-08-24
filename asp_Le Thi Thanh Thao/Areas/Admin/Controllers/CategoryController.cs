using asp_Le_Thi_Thanh_Thao.Context;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static asp_Le_Thi_Thanh_Thao.Common;

namespace asp_Le_Thi_Thanh_Thao.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        QL_BanHangEntities3 objQL_BanHangEntities2 = new QL_BanHangEntities3();
        // GET: Admin/Category
        public ActionResult Index(string currentFilter, string SearchString, int? page)
        {
            var lstCategory = new List<Category>();
            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = currentFilter;
            }
            if (!string.IsNullOrEmpty(SearchString))
            {
                lstCategory = objQL_BanHangEntities2.Categories.Where(n => n.Name.Contains(SearchString)).ToList();
            }
            else
            {
                lstCategory = objQL_BanHangEntities2.Categories.ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            lstCategory = lstCategory.OrderByDescending(n => n.Id).ToList();
            return View(lstCategory.ToPagedList(pageNumber, pageSize));
        }



        void LoadData()
        {

            Common objCommon = new Common();

            var lstCat = objQL_BanHangEntities2.Categories.ToList();

            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dtCategory = converter.ToDataTable(lstCat);
            ViewBag.ListCategory = objCommon.ToSelectList(dtCategory, "Id", "Name");

            List<CategoryType> lstCategoryType = new List<CategoryType>();
            CategoryType objCategoryType = new CategoryType();
            objCategoryType.Id = 1;
            objCategoryType.Name = "Danh mục phổ biến";
            lstCategoryType.Add(objCategoryType);



            DataTable dtCategoryType = converter.ToDataTable(lstCategoryType);
            ViewBag.CategoryType = objCommon.ToSelectList(dtCategoryType, "Id", "Name");
        }
        [HttpGet]
        public ActionResult Create()
        {
            //lay du lieu
            this.LoadData();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category objCategory)
        {
            this.LoadData();
            if (ModelState.IsValid)
            {
                try
                {

                    if (objCategory.ImageUpload != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(objCategory.ImageUpload.FileName);
                        //tenhinh
                        string extension = Path.GetExtension(objCategory.ImageUpload.FileName);
                        //png
                        fileName = fileName + extension;
                        //tenhinh.png
                        objCategory.Avatar = fileName;
                        objCategory.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/category"), fileName));
                    }
                    objCategory.CreatedOnUtc = DateTime.Now;
                    objQL_BanHangEntities2.Categories.Add(objCategory);
                    objQL_BanHangEntities2.SaveChanges();

                    return RedirectToAction("Index");
                }

                catch
                {
                    return View(objCategory);
                }

            }
            return View(objCategory);
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            var objCategory = objQL_BanHangEntities2.Categories.Where(n => n.Id == id).FirstOrDefault();
            return View(objCategory);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objCategory = objQL_BanHangEntities2.Categories.Where(n => n.Id == id).FirstOrDefault();
            return View(objCategory);
        }

        [HttpPost]
        public ActionResult Delete(Category objcat)
        {
            var objCategory = objQL_BanHangEntities2.Categories.Where(n => n.Id == objcat.Id).FirstOrDefault();

            objQL_BanHangEntities2.Categories.Remove(objCategory);
            objQL_BanHangEntities2.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            this.LoadData();
            var objCategory = objQL_BanHangEntities2.Categories.Where(n => n.Id == id).FirstOrDefault();
            return View(objCategory);
        }
        [HttpPost]
        [ValidateInput(false)]
      
        public ActionResult Edit( Category objCategory, FormCollection form)
        {
          
            if (objCategory.ImageUpload != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(objCategory.ImageUpload.FileName);
                //tenhinh
                string extension = Path.GetExtension(objCategory.ImageUpload.FileName);
                //png
                fileName = fileName + extension;
                //tenhinh.png
                objCategory.Avatar = fileName;
                objCategory.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/category"), fileName));

            }
            else
            {
                objCategory.Avatar = form["oldimage"];
                objQL_BanHangEntities2.Entry(objCategory).State = EntityState.Modified;
                objCategory.UpdatedOnUtc = DateTime.Now;
                objQL_BanHangEntities2.SaveChanges();
                return RedirectToAction("Index");
            }
            objCategory.UpdatedOnUtc = DateTime.Now;
            objQL_BanHangEntities2.Entry(objCategory).State = EntityState.Modified;
            objQL_BanHangEntities2.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}