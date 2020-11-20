using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Model;
using BLL;

namespace API.Controllers
{
    public class Cart_APIController : ApiController
    {
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpGet]
        public List<Model.BillInfoModel> GetAllItemInCart(int id)
        {
            return new BillInfoBLL().GetBillInfoByID(id);
        }
        [HttpPost]
        public bool InsertItemInCart(BillInfoModel model)
        {
            return new BillInfoBLL().InsertBillInfor(model);
        }
    }
}
