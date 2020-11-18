using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Model;
using Model.Common;

namespace UI.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        string url;
        ServiceRepository serviceObj;
        public CategoryController()
        {
            serviceObj = new ServiceRepository();
            url = "https://localhost:44379/api/Category_API/";
        }
        [HasPermision(RoleID = "1")]
        public ActionResult Index()
        {
            try
            {
                HttpResponseMessage response = serviceObj.GetResponse(url + "GetAllCategories");
                response.EnsureSuccessStatusCode();
                List<CategoryModel> list = response.Content.ReadAsAsync<List<CategoryModel>>().Result;
                return View(list);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public ActionResult EditCategory(int id)
        {
            HttpResponseMessage response = serviceObj.GetResponse(url + "GetDetailCategory/" + id);
            response.EnsureSuccessStatusCode();
            CategoryModel cate = response.Content.ReadAsAsync<CategoryModel>().Result;
            return View(cate);
        }
        [HttpPost]
        public ActionResult UpdateCategory(CategoryModel cate)
        {
            HttpResponseMessage response = serviceObj.PutResponse(url + "UpdateCategory/", cate);
            response.EnsureSuccessStatusCode();
            return RedirectToAction("Index");
        }
        public ActionResult DetailCategory(int id)
        {
            HttpResponseMessage response = serviceObj.GetResponse(url + "GetDetailCategory/" + id);
            response.EnsureSuccessStatusCode();
            CategoryModel cate = response.Content.ReadAsAsync<CategoryModel>().Result;
            return View(cate);
        }
        [HttpGet]
        public ActionResult CreateCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateCategory(CategoryModel cate)
        {
            HttpResponseMessage response = serviceObj.PostResponse(url + "InsertCategory/", cate);
            response.EnsureSuccessStatusCode();
            return RedirectToAction("Index");
        }
        public ActionResult DeleteCategory(int id)
        {
            HttpResponseMessage response = serviceObj.DeleteResponse(url + "DeleteCategory/" + id);
            response.EnsureSuccessStatusCode();
            return RedirectToAction("Index");
        }
    }
}