using BLL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace API.Controllers
{
    public class Product_APIController : ApiController
    {
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpGet]
        public List<ProductModel> GetAllProducts()
        {
            return new ProductBLL().GetAllProducts();
        }
        public ProductModel GetProductByID(int id)
        {
            return new ProductBLL().GetProductByID(id);
        }
        public List<ProductModel> GetProductsByCateID(int id)
        {
            return new ProductBLL().GetProductsByCateID(id);
        }
        public List<ProductModel> GetNewProductsByCount(int count)
        {
            return new ProductBLL().GetNewProductsByCount(count);
        }
        public List<ProductModel> GetProductsBySearch(string tim)
        {
            return new ProductBLL().GetProductsBySearch(tim);
        }
        public ProductModel GetPriceProductByID(int id)
        {
            return new ProductBLL().GetPriceProductByID(id);
        }
    }
}