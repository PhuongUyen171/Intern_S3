using log4net;
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
                if (model != null)
                {
                    model.FullName = new UserController().GetCustomerByUsername(model.UserName).CustomName;
                    Session[Constants.USER_SESSION] = model;
                }
                int num = 8;
                HttpResponseMessage response = serviceObj.GetResponse(url + "api/Product_API/GetNewProductsByCount?count=" + num);
                response.EnsureSuccessStatusCode();
                List<ProductModel> list = response.Content.ReadAsAsync<List<ProductModel>>().Result;
                log.Info("Connect to index page.");
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
                return View();
            }
            catch (System.Exception)
            {
                log.Error("Unable to find page.");
                return View("404.cshtml");
            }
        }

        public ActionResult HuongDanMuaHang()
        {
            try
            {
                return View();
            }
            catch (System.Exception)
            {
                log.Error("Unable to find page.");
                return View("404.cshtml");
            }
        }

        public ActionResult GioiThieu()
        {
            try
            {
                return View();
            }
            catch (System.Exception)
            {
                log.Error("Unable to find page.");
                return View("404.cshtml");
            }
        }

        public ActionResult TuyenDung()
        {
            try
            {
                return View();
            }
            catch (System.Exception)
            {
                log.Error("Unable to find page.");
                return View("404.cshtml");
            }
        }

        public ActionResult HeThong()
        {
            try
            {
                return View();
            }
            catch (System.Exception)
            {
                log.Error("Unable to find page.");
                return View("404.cshtml");
            }
        }

        public ActionResult VanHoa()
        {
            try
            {
                return View();
            }
            catch (System.Exception)
            {
                log.Error("Unable to find page.");
                return View("404.cshtml");
            }
        }
        #endregion
        public ActionResult TinTuc()
        {
            try
            {
                url = "https://localhost:44379/";
                serviceObj = new ServiceRepository();
                HttpResponseMessage response = serviceObj.GetResponse(url + "api/News_API/GetAllNews");
                response.EnsureSuccessStatusCode();
                List<Model.NewsModel> list = response.Content.ReadAsAsync<List<Model.NewsModel>>().Result;
                return View(list);
            }
            catch (System.Exception)
            {
                log.Error("Unable to connect page.");
                throw;
            }
            
        }
        #region PartialView
        public ActionResult CategoryPartial()
        {
            //try
            //{
                int num = 9;
                HttpResponseMessage response = serviceObj.GetResponse(url + "api/Category_API/GetCategoryByCount?count=" + num);
                response.EnsureSuccessStatusCode();
                List<CategoryModel> list = response.Content.ReadAsAsync<List<CategoryModel>>().Result;
                return View(list);
            //}
            //catch (System.Exception)
            //{
            //    log.Error("Unable to load partial.");
            //    throw;
            //}
            
        }
        public ActionResult SearchPartial()
        {
            try
            {
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
                int num = 3;
                url = "https://localhost:44379/";
                ServiceRepository serviceObj = new ServiceRepository();
                HttpResponseMessage response = serviceObj.GetResponse(url + "api/News_API/GetNewsByCount?count=" + num);
                response.EnsureSuccessStatusCode();
                List<NewsModel> list = response.Content.ReadAsAsync<List<NewsModel>>().Result;
                return View(list);
            }
            catch (System.Exception)
            {
                log.Error("Unable to connect partial.");
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
            return result;
        }
        public ActionResult ChiNhanh(string local)
        {
            try
            {
                url = "https://localhost:44379/";
                serviceObj = new ServiceRepository();
                HttpResponseMessage response = serviceObj.GetResponse(url + "api/Store_API/GetStoreByLocation?local=" + local);
                response.EnsureSuccessStatusCode();
                List<StoreModel> list = response.Content.ReadAsAsync<List<StoreModel>>().Result;
                return View(list);
            }
            catch (System.Exception)
            {
                log.Error("Unable to connect page.");
                throw;
            }
            
        }
        public ActionResult GetDetailNews(int id)
        {
            try
            {
                url = "https://localhost:44379/";
                serviceObj = new ServiceRepository();
                HttpResponseMessage response = serviceObj.GetResponse(url + "api/News_API/GetDetailNews/" + id);
                response.EnsureSuccessStatusCode();
                NewsModel result = response.Content.ReadAsAsync<NewsModel>().Result;
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