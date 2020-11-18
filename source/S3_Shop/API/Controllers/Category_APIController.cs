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
    public class Category_APIController : ApiController
    {
        // GET: api/Category
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpGet]
        public List<Model.CategoryModel> GetAllCategories()
        {
            return new CategoryBLL().GetAllCategories();
        }
        public Model.CategoryModel GetCategoryByID(int id)
        {
            return new CategoryBLL().GetCategoryByID(id);
        }
        // GET: api/Category/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST: api/Category
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Category/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Category/5
        public void Delete(int id)
        {
        }
    }
}
