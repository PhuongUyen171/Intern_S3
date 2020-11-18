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
    public class ProductAdminController : Controller
    {
        string url;
        ServiceRepository serviceObj;
        public ProductAdminController()
        {
            serviceObj = new ServiceRepository();
            url = "https://localhost:44379/api/Product_API/";
        }
        //[HasPermision(RoleID = "1")]
        public ActionResult Index()
        {
            try
            {
                HttpResponseMessage response = serviceObj.GetResponse(url + "GetAllProducts");
                response.EnsureSuccessStatusCode();
                List<ProductModel> list = response.Content.ReadAsAsync<List<ProductModel>>().Result;
                return View(list);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public ActionResult EditProduct(int id)
        {
            HttpResponseMessage response = serviceObj.GetResponse(url + "GetDetailProduct/" + id);
            response.EnsureSuccessStatusCode();
            ProductModel pro = response.Content.ReadAsAsync<ProductModel>().Result;
            return View(pro);
        }
        [HttpPost]
        public ActionResult UpdateProduct(ProductModel pro)
        {
            HttpResponseMessage response = serviceObj.PutResponse(url + "UpdateProduct/", pro);
            response.EnsureSuccessStatusCode();
            return RedirectToAction("Index");
        }
        public ActionResult DetailProduct(int id)
        {
            HttpResponseMessage response = serviceObj.GetResponse(url + "GetDetailProduct/" + id);
            response.EnsureSuccessStatusCode();
            ProductModel pro = response.Content.ReadAsAsync<ProductModel>().Result;
            return View(pro);
        }
        [HttpGet]
        public ActionResult CreateProduct()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateProduct(ProductModel pro)
        {
            HttpResponseMessage response = serviceObj.PostResponse(url + "InsertProduct/", pro);
            response.EnsureSuccessStatusCode();
            return RedirectToAction("Index");
        }
        public ActionResult DeleteProduct(int id)
        {
            HttpResponseMessage response = serviceObj.DeleteResponse(url + "DeleteProduct/" + id);
            response.EnsureSuccessStatusCode();
            return RedirectToAction("Index");
        }
    }
}