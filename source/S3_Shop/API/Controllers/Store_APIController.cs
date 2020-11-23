using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Model;

namespace API.Controllers
{
    public class Store_APIController : ApiController
    {
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpGet]
        public List<StoreModel> GetAllStores()
        {
            return new StoreBLL().GetAllStores();
        }
        public StoreModel GetStoreByID(int id)
        {
            return new StoreBLL().GetStoreByID(id);
        }
        public List<StoreModel> GetStoreByLocation(string local)
        {
            return new StoreBLL().GetStoresByLocation(local);
        }
    }
}