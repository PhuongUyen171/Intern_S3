using Model;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;
using UI.Helpers;

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
            List<ProductModel> list = responseListProduct.Content.ReadAsAsync<List<ProductModel>>().Result;
            if (list.Count!=0)
            {
                //Tên danh mục
                HttpResponseMessage responseCategory = serviceObj.GetResponse(url + "api/Category_API/GetCategoryByID/" + list.First().CateID);
                CategoryModel cate = responseCategory.Content.ReadAsAsync<CategoryModel>().Result;
                if (cate != null)
                {
                    ViewBag.Loai = cate.CateName;
                    return View(list);
                }
            }
            return View("~/Views/Shared/ProductNotFound.cshtml");
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