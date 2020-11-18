using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Model;
using System.Web.Http.Cors;

namespace API.Areas.Admin.Controllers
{
    public class Voucher_APIController : ApiController
    {
        private VoucherBLL vouBll = new VoucherBLL();
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpGet]
        public List<VoucherModel> GetAllVouchers()
        {
            return vouBll.GetAllVouchers();
        }
    }
}
