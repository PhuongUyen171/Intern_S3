using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace UI.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetDetailProduct(int id)
        {
            var url = "https://localhost:44379/";
            ServiceRepository serviceObj = new ServiceRepository();
            HttpResponseMessage response = serviceObj.GetResponse(url + "api/Product_API/GetProductByID/" + id);
            response.EnsureSuccessStatusCode();
            Model.ProductModel result = response.Content.ReadAsAsync<Model.ProductModel>().Result;
            return View(result);
        }
        public ActionResult GetProductsByCateID(int id)
        {
            var url = "https://localhost:44379/";
            ServiceRepository serviceObj = new ServiceRepository();
            //List sản phẩm
            HttpResponseMessage responseListProduct = serviceObj.GetResponse(url + "api/Product_API/GetProductsByCateID/" + id);
            responseListProduct.EnsureSuccessStatusCode();
            List<Model.ProductModel> list = responseListProduct.Content.ReadAsAsync<List<Model.ProductModel>>().Result;
            //Tên danh mục
            HttpResponseMessage responseCategory= serviceObj.GetResponse(url + "api/Category/GetCategoryByID/" + list.First().CategoryID);
            Model.CategoryModel cate = responseCategory.Content.ReadAsAsync<Model.CategoryModel>().Result;
            if (cate != null)
            {
                ViewBag.Loai = cate.CateName;
                return View(list);
            }
            return this.View();
        }
        [HttpPost]
        public ActionResult SearchProducts(FormCollection c)
        {
            var url = "https://localhost:44379/";
            ServiceRepository serviceObj = new ServiceRepository();
            //List sản phẩm
            var tim = c["searchText"];
            HttpResponseMessage responseListProduct = serviceObj.GetResponse(url + "api/Product_API/GetProductsBySearch?tim=" + tim);
            responseListProduct.EnsureSuccessStatusCode();
            List<Model.ProductModel> list = responseListProduct.Content.ReadAsAsync<List<Model.ProductModel>>().Result;
            return View(list);
        }
    }
}