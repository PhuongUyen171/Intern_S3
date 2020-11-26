using log4net;
using Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;
using UI.Helpers;

namespace UI.Areas.Admin.Controllers
{
    public class CustomerController : Controller
    {
        private string url;
        private ServiceRepository serviceObj;
        private static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public CustomerController()
        {
            serviceObj = new ServiceRepository();
            url = "https://localhost:44379/api/Customer_API/";
        }
        //[HasPermision(RoleID = "1")]
        public ActionResult Index()
        {
            try
            {
                HttpResponseMessage response = serviceObj.GetResponse(url + "GetAllCustomers");
                response.EnsureSuccessStatusCode();
                List<CustomerModel> list = response.Content.ReadAsAsync<List<CustomerModel>>().Result;
                return View(list);
            }
            catch (Exception)
            {
                log.Error("Cannot connect to customer page.");
                throw;
            }
        }
    }
}