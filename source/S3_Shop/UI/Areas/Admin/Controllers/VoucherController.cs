using API.Models;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace UI.Areas.Admin.Controllers
{
    public class VoucherController : Controller
    {
        string url;
        ServiceRepository serviceObj;
        public VoucherController()
        {
            serviceObj = new ServiceRepository();
            url = "https://localhost:44379/api/Voucher_API/";
        }
        public ActionResult Index()
        {
            try
            {
                HttpResponseMessage response = serviceObj.GetResponse(url + "GetAllVouchers");
                response.EnsureSuccessStatusCode();
                List<VoucherModel> list = response.Content.ReadAsAsync<List<VoucherModel>>().Result;
                return View(list);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}