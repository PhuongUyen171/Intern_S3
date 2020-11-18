using API.Controllers;
using API.Models;
using Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using UI.Models;

namespace UI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            UserLogin model = CheckAccount();
            if (model != null)
            {
                model.FullName = new UserController().GetCustomerByUsername(model.UserName).CustomName;
                Session[Constants.USER_SESSION] = model;
            }
            int num = 8;
            var url = "https://localhost:44379/";
            ServiceRepository serviceObj = new ServiceRepository();
            HttpResponseMessage response = serviceObj.GetResponse(url + "api/Product_API/GetNewProductsByCount?count="+num);
            response.EnsureSuccessStatusCode();
            List<Model.ProductModel> list = response.Content.ReadAsAsync<List<Model.ProductModel>>().Result;
            return View(list);
        }

        #region trang tĩnh
        public ActionResult ChinhSachBanHang()
        {
            return View();
        }

        public ActionResult HuongDanMuaHang()
        {
            return View();
        }

        public ActionResult GioiThieu()
        {
            return View();
        }

        public ActionResult TuyenDung()
        {
            return View();
        }

        public ActionResult HeThong()
        {
            return View();
        }

        public ActionResult VanHoa()
        {
            return View();
        }
        #endregion

        public ActionResult TinTuc()
        {
            var url = "https://localhost:44379/";
            ServiceRepository serviceObj = new ServiceRepository();
            HttpResponseMessage response = serviceObj.GetResponse(url + "api/News");
            response.EnsureSuccessStatusCode();
            List<Model.NewsModel> list = response.Content.ReadAsAsync<List<Model.NewsModel>>().Result;
            
            return View(list);
        }
        #region PartialView
        public ActionResult CategoryPartial()
        {
            var url = "https://localhost:44379/";
            ServiceRepository serviceObj = new ServiceRepository();
            HttpResponseMessage response = serviceObj.GetResponse(url + "api/Category_API/GetAllCategories");
            response.EnsureSuccessStatusCode();
            List<Model.CategoryModel> list = response.Content.ReadAsAsync<List<Model.CategoryModel>>().Result;

            return View(list);
        }
        public ActionResult SearchPartial()
        {
            return PartialView();
        }
        #endregion

        public UserLogin CheckAccount()
        {
            UserLogin result = null;
            string username = string.Empty;
            string id = string.Empty;
            string fullname = string.Empty;
            if (Request.Cookies["usernameCustomer"] != null)
                username = Request.Cookies["usernameCustomer"].Value;
            if (Request.Cookies["idCustomer"] != null)
                id = Request.Cookies["idCustomer"].Value;
            if (Request.Cookies["nameCustomer"] != null)
                fullname = Request.Cookies["nameCustomer"].Value;
            if (!string.IsNullOrEmpty(username) & !string.IsNullOrEmpty(id) & !string.IsNullOrEmpty(fullname))
                result = new UserLogin { UserID = int.Parse(id), UserName = username, FullName = fullname };
            return result;
        }
        //public ActionResult ChiNhanhHaNoi()
        //{
        //    var hn = data.CUAHANGs.Where(m => m.Vung == "HN");
        //    return View(hn);
        //}
        //public ActionResult ChiNhanhHCM()
        //{
        //    var hcm = data.CUAHANGs.Where(m => m.Vung == "HCM");
        //    return View(hcm);
        //}
        //public ActionResult TinTuc()
        //{
        //    var tintuc = from tt in data.TINTUCs select tt;
        //    return View(tintuc);
        //}
        //public ActionResult ChiTietTinTuc(string id)
        //{
        //    var chitiet = data.TINTUCs.First(m => m.MaTin == id);
        //    return View(chitiet);
        //}
    }
}