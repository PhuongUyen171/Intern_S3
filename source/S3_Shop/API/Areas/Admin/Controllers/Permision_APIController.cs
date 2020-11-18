using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Model;

namespace API.Areas.Admin.Controllers
{
    public class Permision_APIController : ApiController
    {
        private PermisionBLL perBll = new PermisionBLL();
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpGet]
        public List<PermisionModel> GetAllPermisions()
        {
            return perBll.GetAllPermisions();
        }
    }
}
