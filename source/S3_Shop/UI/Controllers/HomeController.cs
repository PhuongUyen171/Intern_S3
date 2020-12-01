
using log4net;
using Model;
using Model.Common;
using System;
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
        private static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private ServiceRepository serviceObj;
        public HomeController()
        {
            url = "https://localhost:44379/";
            serviceObj = new ServiceRepository();
        }
        public ActionResult Index()
        {
            try
            {

                UserLogin model = CheckAccount();

                if (model != null && Session[Constants.TOKEN_NUMBER] != null)
                {
                    model.FullName = new UserController().GetCustomerByUsername(model.UserName).CustomName;
                    Session[Constants.USER_SESSION] = model;
                    log.Info("User is on cookies.");
                }
                int num = 8;
                HttpResponseMessage response = serviceObj.GetResponse(url + "api/Product_API/GetNewProductsByCount?count=" + num);
                response.EnsureSuccessStatusCode();
                List<ProductModel> list = response.Content.ReadAsAsync<List<ProductModel>>().Result;
                log.Info("Connect to index page successful.");
                return View(list);
            }
            catch (System.Exception)
            {
                log.Error("Unable to connect page.");
                return View("404");
            }
        }

        #region trang tĩnh
        public ActionResult ChinhSachBanHang()
        {
            try
            {
                log.Info("Load page ChinhSachBanHang successfully.");
                return View();
            }
            catch (System.Exception)
            {
                log.Error("Unable to find page: Chinh sach ban hang.");
                return View("404.cshtml");
            }
        }

        public ActionResult HuongDanMuaHang()
        {
            try
            {
                log.Info("Load page HuongDanMuaHang successfully.");
                return View();
            }
            catch (System.Exception)
            {
                log.Error("Unable to find page: HuongDanMuaHang.");
                return View("404.cshtml");
            }
        }

        public ActionResult GioiThieu()
        {
            try
            {
                log.Info("Load page Introduce successfully.");
                return View();
            }
            catch (System.Exception)
            {
                log.Error("Unable to find page: Introduce");
                return View("404.cshtml");
            }
        }

        public ActionResult TuyenDung()
        {
            try
            {
                log.Info("Load page TuyenDung successfully.");
                return View();
            }
            catch (System.Exception)
            {
                log.Error("Unable to find page: TuyenDung.");
                return View("404.cshtml");
            }
        }

        public ActionResult HeThong()
        {
            try
            {
                log.Info("Load page HeThong successfully.");
                return View();
            }
            catch (System.Exception)
            {
                log.Error("Unable to find page: HeThong.");
                return View("404.cshtml");
            }
        }

        public ActionResult VanHoa()
        {
            try
            {
                log.Info("Load page VanHoa successfully.");
                return View();
            }
            catch (System.Exception)
            {
                log.Error("Unable to find page: VanHoa.");
                return View("404.cshtml");
            }
        }
        #endregion
        public ActionResult TinTuc()
        {
            try
            {
                log.Info("Prepare to connect page TinTuc.");
                url = "https://localhost:44379/";
                serviceObj = new ServiceRepository();
                HttpResponseMessage response = serviceObj.GetResponse(url + "api/News_API/GetAllNews");
                response.EnsureSuccessStatusCode();
                List<Model.NewsModel> list = response.Content.ReadAsAsync<List<Model.NewsModel>>().Result;
                log.Info("Get list news successfully.");
                return View(list);
            }
            catch (System.Exception)
            {
                log.Error("Unable to connect page TinTuc");
                throw;
            }
            
        }
        #region PartialView
        public ActionResult CategoryPartial()
        {
            try
            {
                log.Info("Prepare to load CategoryPartial");
                int num = 9;
                HttpResponseMessage response = serviceObj.GetResponse(url + "api/Category_API/GetCategoryByCount?count=" + num);
                response.EnsureSuccessStatusCode();
                List<CategoryModel> list = response.Content.ReadAsAsync<List<CategoryModel>>().Result;
                log.Info("Get list category successfully.");
                return View(list);
            }
            catch (Exception ex)
            {
                log.Error("Unable to load category partial ");
                throw;
            }
        }
        public ActionResult SearchPartial()
        {
            try
            {
                log.Info("Load search partial successfully.");
                return PartialView();
            }
            catch (System.Exception)
            {
                log.Error("Unable to connect partial.");
                throw;
            }
        }
        public ActionResult NewsPartial()
        {
            try
            {
                log.Info("Prepare to load NewsPartial");
                int num = 3;
                url = "https://localhost:44379/";
                ServiceRepository serviceObj = new ServiceRepository();
                HttpResponseMessage response = serviceObj.GetResponse(url + "api/News_API/GetNewsByCount?count=" + num);
                response.EnsureSuccessStatusCode();
                List<NewsModel> list = response.Content.ReadAsAsync<List<NewsModel>>().Result;
                log.Info("Get list news successfully.");
                return View(list);
            }
            catch (System.Exception)
            {
                log.Error("Unable to load news partial.");
                throw;
            }
            
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
            log.Info("Response result check account is using.");
            return result;
        }
        public ActionResult ChiNhanh(string local)
        {
            try
            {
                log.Info("Prepare to load page ChiNhanh.");
                url = "https://localhost:44379/";
                serviceObj = new ServiceRepository();
                HttpResponseMessage response = serviceObj.GetResponse(url + "api/Store_API/GetStoreByLocation?local=" + local);
                response.EnsureSuccessStatusCode();
                List<StoreModel> list = response.Content.ReadAsAsync<List<StoreModel>>().Result;
                log.Info("Get list stores successfully.");
                return View(list);
            }
            catch (System.Exception)
            {
                log.Error("Unable to load page ChiNhanh.");
                throw;
            }
            
        }
        public ActionResult GetDetailNews(int id)
        {
            try
            {
                log.Info("Prepare to get detail new");
                url = "https://localhost:44379/";
                serviceObj = new ServiceRepository();
                HttpResponseMessage response = serviceObj.GetResponse(url + "api/News_API/GetDetailNews/" + id);
                response.EnsureSuccessStatusCode();
                NewsModel result = response.Content.ReadAsAsync<NewsModel>().Result;
                log.Info("Get information news by id successfully.");
                return View(result);
            }
            catch (System.Exception)
            {
                log.Error("Unable to connect page.");
                throw;
            }
        }
    }
}