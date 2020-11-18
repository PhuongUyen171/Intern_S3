using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Model.Common;

namespace UI.Areas.Admin.Controllers
{
    public class StoreController : Controller
    {
        private ServiceRepository serviceObj;
        private string url;
        public StoreController()
        {
            url = "https://localhost:44379/api/Store_API/";
            serviceObj = new ServiceRepository();
        }
        // GET: Admin/Role
        [HasPermision(RoleID = "2")]
        public ActionResult Index()
        {
            try
            {
                HttpResponseMessage response = serviceObj.GetResponse(url + "GetAllStores");
                response.EnsureSuccessStatusCode();
                List<Model.StoreModel> list = response.Content.ReadAsAsync<List<Model.StoreModel>>().Result;
                return View(list);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult DetailStore(int id)
        {
            HttpResponseMessage response = serviceObj.GetResponse(url + "GetStoreByID/" + id);
            response.EnsureSuccessStatusCode();
            Model.StoreModel store = response.Content.ReadAsAsync<Model.StoreModel>().Result;
            return View(store);
        }
    }
}