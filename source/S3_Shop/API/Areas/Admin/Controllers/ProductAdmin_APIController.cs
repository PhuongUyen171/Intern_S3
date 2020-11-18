using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Model;
using Antlr.Runtime.Tree;

namespace API.Areas.Admin.Controllers
{
    public class ProductAdmin_APIController : ApiController
    {
        private ProductBLL proBll = new ProductBLL();
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpGet]
        public List<ProductModel> GetAllProducts()
        {
            return proBll.GetAllProducts();
        }
        public ProductModel GetDetailProduct(int id)
        {
            return proBll.GetProductByID(id);
        }
        public bool InsertProduct(ProductModel proInsert)
        {
            return proBll.InsertProduct(proInsert);
        }
        public bool DeleteProduct(int id)
        {
            return proBll.DeleteProduct(id);
        }
        public bool UpdateProduct(ProductModel proUpdate)
        {
            return proBll.UpdateProduct(proUpdate);
        }
    }
}
