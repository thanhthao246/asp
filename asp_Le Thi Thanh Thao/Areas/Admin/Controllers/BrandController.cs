using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using asp_Le_Thi_Thanh_Thao.Context;
using System.Web.Mvc;
using PagedList;
using System.IO;
using System.Data.Entity;

namespace asp_Le_Thi_Thanh_Thao.Areas.Admin.Controllers
{
        public class BrandController : Controller
        {
            QL_BanHangEntities3 objQL_BanHangEntities2 = new QL_BanHangEntities3();
            public ActionResult Index(string currentFilter, string SearchString, int? page)
            {
                var lstBrands = new List<Brand>();
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
                    lstBrands = objQL_BanHangEntities2.Brands.Where(n => n.Name.Contains(SearchString)).ToList();
                }
                else
                {
                    lstBrands = objQL_BanHangEntities2.Brands.ToList();
                }
                ViewBag.CurrentFilter = SearchString;
                int pageSize = 4;
                int pageNumber = (page ?? 1);
                lstBrands = lstBrands.OrderByDescending(n => n.Id).ToList();
                return View(lstBrands.ToPagedList(pageNumber, pageSize));
            }
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(Brand brand)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    if (brand.ImageUpload != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(brand.ImageUpload.FileName);
                        string extension = Path.GetExtension(brand.ImageUpload.FileName);
                        fileName = fileName + extension;
                        brand.Avatar = fileName;
                        brand.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/category"), fileName));
                    }
                    brand.CreatedOnUtc = DateTime.Now;
                    objQL_BanHangEntities2.Brands.Add(brand);
                    objQL_BanHangEntities2.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch
                {
                    return RedirectToAction("Index");
                }
            }
            return View(brand);
        }
        [HttpGet]
            public ActionResult Details(int id)
            {
                var product = objQL_BanHangEntities2.Brands.Where(n => n.Id == id).FirstOrDefault();
                return View(product);
            }

        [HttpGet]
            public ActionResult Delete(int id)
            {
                var product = objQL_BanHangEntities2.Brands.Where(n => n.Id == id).FirstOrDefault();
                return View(product);
            }
            [HttpPost]
            public ActionResult Delete(Brand objCat)
            {
                var objBrand = objQL_BanHangEntities2.Brands.Where(n => n.Id == objCat.Id).FirstOrDefault();
                objQL_BanHangEntities2.Brands.Remove(objBrand);
                objQL_BanHangEntities2.SaveChanges();


                return RedirectToAction("Index");
            }

            

        [HttpGet]
            public ActionResult Edit(int id)
            {
                var Brand = objQL_BanHangEntities2.Brands.Where(n => n.Id == id).FirstOrDefault();
                return View(Brand);
            }

        [HttpPost, ValidateInput(false)]
            public ActionResult Edit(int id, Brand objBrand, FormCollection form)
            {
                if (objBrand.ImageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(objBrand.ImageUpload.FileName);
                    string extension = Path.GetExtension(objBrand.ImageUpload.FileName);
                    fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                    objBrand.Avatar = fileName;
                    objBrand.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/category"), fileName));
                }
                else
                {
                    objBrand.Avatar = form["oldimage"];
                    objQL_BanHangEntities2.Entry(objBrand).State = EntityState.Modified;
                    objBrand.UpdateOnUtc = DateTime.Now;
                    objQL_BanHangEntities2.SaveChanges();
                    return RedirectToAction("Index");

                }
                objBrand.UpdateOnUtc = DateTime.Now;
                objQL_BanHangEntities2.Entry(objBrand).State = EntityState.Modified;
                objQL_BanHangEntities2.SaveChanges();


                return RedirectToAction("Index");
        }
    }

}
