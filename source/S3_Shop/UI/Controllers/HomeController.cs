using Model;
using Model.Common;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;
using UI.Helpers;
using UI.Models;

namespace UI.Controllers
{
    public class HomeController : Controller
    {
        private string url;
        public ActionResult Index()
        {
            UserLogin model = CheckAccount();
            if (model != null)
            {
                model.FullName = new UserController().GetCustomerByUsername(model.UserName).CustomName;
                Session[Constants.USER_SESSION] = model;
            }
            int num = 8;
            url = "https://localhost:44379/";
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
            url = "https://localhost:44379/";
            ServiceRepository serviceObj = new ServiceRepository();
            HttpResponseMessage response = serviceObj.GetResponse(url + "api/News_API/GetAllNews");
            response.EnsureSuccessStatusCode();
            List<Model.NewsModel> list = response.Content.ReadAsAsync<List<Model.NewsModel>>().Result;
            return View(list);
        }
        #region PartialView
        public ActionResult CategoryPartial()
        {
            int num = 9;
            url = "https://localhost:44379/";
            ServiceRepository serviceObj = new ServiceRepository();
            HttpResponseMessage response = serviceObj.GetResponse(url + "api/Category_API/GetCategoryByCount?count="+num);
            response.EnsureSuccessStatusCode();
            List<CategoryModel> list = response.Content.ReadAsAsync<List<CategoryModel>>().Result;
            return View(list);
        }
        public ActionResult SearchPartial()
        {
            return PartialView();
        }
        public ActionResult NewsPartial()
        {
            int num = 3;
            url = "https://localhost:44379/";
            ServiceRepository serviceObj = new ServiceRepository();
            HttpResponseMessage response = serviceObj.GetResponse(url + "api/News_API/GetNewsByCount?count=" + num);
            response.EnsureSuccessStatusCode();
            List<NewsModel> list = response.Content.ReadAsAsync<List<NewsModel>>().Result;
            return View(list);
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
        public ActionResult ChiNhanh(string local)
        {
            url = "https://localhost:44379/";
            ServiceRepository serviceObj = new ServiceRepository();
            HttpResponseMessage response = serviceObj.GetResponse(url + "api/Store_API/GetStoreByLocation?local=" + local);
            response.EnsureSuccessStatusCode();
            List<StoreModel> list = response.Content.ReadAsAsync<List<StoreModel>>().Result;
            return View(list);
        }
        public ActionResult GetDetailNews(int id)
        {
            url = "https://localhost:44379/";
            ServiceRepository serviceObj = new ServiceRepository();
            HttpResponseMessage response = serviceObj.GetResponse(url + "api/News_API/GetDetailNews/"+id);
            response.EnsureSuccessStatusCode();
            NewsModel result = response.Content.ReadAsAsync<NewsModel>().Result;
            return View(result);
        }
        
    }
}