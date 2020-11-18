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
    public class Store_APIController : ApiController
    {
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpGet]
        public List<Model.StoreModel> GetAllStores()
        {
            return new StoreBLL().GetAllStores();
        }
        public Model.StoreModel GetStoreByID(int id)
        {
            return new StoreBLL().GetStoreByID(id);
        }
    }
}