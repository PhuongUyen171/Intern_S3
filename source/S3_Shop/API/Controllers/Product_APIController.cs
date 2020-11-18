using BLL;
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
        public List<Model.ProductModel> GetAllProducts()
        {
            return new ProductBLL().GetAllProducts();
        }
        public Model.ProductModel GetProductByID(int id)
        {
            return new ProductBLL().GetProductByID(id);
        }
        public List<Model.ProductModel> GetProductsByCateID(int id)
        {
            return new ProductBLL().GetProductsByCateID(id);
        }
        public List<Model.ProductModel> GetNewProductsByCount(int count)
        {
            return new ProductBLL().GetNewProductsByCount(count);
        }
        public List<Model.ProductModel> GetProductsBySearch(string tim)
        {
            return new ProductBLL().GetProductsBySearch(tim);
        }
    }
}