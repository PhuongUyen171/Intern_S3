using Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;
using UI.Helpers;

namespace UI.Areas.Admin.Controllers
{
    public class EmployeeController : Controller
    {
        string url;
        ServiceRepository serviceObj;
        public EmployeeController()
        {
            serviceObj = new ServiceRepository();
            url = "https://localhost:44379/api/Employee_API/";
        }
        //[HasPermision(RoleID = "1")]
        public ActionResult Index()
        {
            try
            {
                HttpResponseMessage response = serviceObj.GetResponse(url + "GetAllEmployees");
                response.EnsureSuccessStatusCode();
                List<EmployeeModel> list = response.Content.ReadAsAsync<List<EmployeeModel>>().Result;
                return View(list);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}