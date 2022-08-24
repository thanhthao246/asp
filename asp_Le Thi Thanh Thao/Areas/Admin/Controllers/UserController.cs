using asp_Le_Thi_Thanh_Thao.Context;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using static asp_Le_Thi_Thanh_Thao.Common;

namespace asp_Le_Thi_Thanh_Thao.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        QL_BanHangEntities3 objQL_BanHangEntities2 = new QL_BanHangEntities3();
        public ActionResult Index(string currentFilter, string SearchString, int? page)
        {
            var lstUser = new List<User>();
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
                lstUser = objQL_BanHangEntities2.Users.Where(n => n.FirstName.Contains(SearchString)).ToList();
            }
            else
            {
                lstUser = objQL_BanHangEntities2.Users.ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            lstUser = lstUser.OrderByDescending(n => n.Id).ToList();
            return View(lstUser.ToPagedList(pageNumber, pageSize));
        }
        void LoadData()
        {
            Common objCommon = new Common();
            var lstUser = objQL_BanHangEntities2.Users.ToList();
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dtUser = converter.ToDataTable(lstUser);
            ViewBag.ListUser = objCommon.ToSelectList(dtUser, "Id", "IsAdmin");


            List<UserType> lstUserType = new List<UserType>();
            UserType objUserType = new UserType();
            objUserType.Id = 1;
            objUserType.IsAdmin = "Admin";
            lstUserType.Add(objUserType);

            objUserType = new UserType();
            objUserType.Id = 2;
            objUserType.IsAdmin = "Khách hàng";
            lstUserType.Add(objUserType);

            DataTable dtUserType = converter.ToDataTable(lstUserType);
            ViewBag.UserType = objCommon.ToSelectList(dtUserType, "Id", "IsAdmin");

        }
        [HttpGet]
        public ActionResult Create()
        {
            this.LoadData();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User _user)
        {
            if (ModelState.IsValid)
            {
                var check = objQL_BanHangEntities2.Users.FirstOrDefault(s => s.Email == _user.Email);
                if (check == null)
                {
                    _user.Password = GetMD5(_user.Password);
                    _user.IsAdmin = false; //IsAdmin = false = người dùng
                    objQL_BanHangEntities2.Configuration.ValidateOnSaveEnabled = false;
                    // add user
                    objQL_BanHangEntities2.Users.Add(_user);
                    //lưu thông tin lại
                    objQL_BanHangEntities2.SaveChanges();
                    // trả về trang chủ
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Email đã tồn tại";


                    return View();
                }

            }
            return View();
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var User = objQL_BanHangEntities2.Users.Where(n => n.Id == id).FirstOrDefault();
            return View(User);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var User = objQL_BanHangEntities2.Users.Where(n => n.Id == id).FirstOrDefault();
            return View(User);
        }
        [HttpPost]
        public ActionResult Delete(User objuser)
        {
            var objUser = objQL_BanHangEntities2.Users.Where(n => n.Id == objuser.Id).FirstOrDefault();
            objQL_BanHangEntities2.Users.Remove(objUser);
            objQL_BanHangEntities2.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            this.LoadData();

            var User = objQL_BanHangEntities2.Users.Where(n => n.Id == id).FirstOrDefault();
            return View(User);
        }
        // mã hóa pass word
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;
            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");
            }
            return byte2String;

        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(int id, User objUser)
        {
            objQL_BanHangEntities2.Entry(objUser).State = EntityState.Modified;
            objQL_BanHangEntities2.SaveChanges();

            return RedirectToAction("Index");
        }


    }
}
