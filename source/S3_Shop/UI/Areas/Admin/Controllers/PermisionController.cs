using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using API.Models;
using Model;

namespace UI.Areas.Admin.Controllers
{
    public class PermisionController : Controller
    {
        string url;
        ServiceRepository serviceObj;
        public PermisionController()
        {
            serviceObj = new ServiceRepository();
            url = "https://localhost:44379/api/Permision_API/";
        }
        public ActionResult Index()
        {
            try
            {
                HttpResponseMessage response = serviceObj.GetResponse(url + "GetAllPermisions");
                response.EnsureSuccessStatusCode();
                List<PermisionModel> list = response.Content.ReadAsAsync<List<PermisionModel>>().Result;
                return View(list);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public ActionResult EditPermision(string groupID, int roleID)
        {
            HttpResponseMessage response = serviceObj.GetResponse(url + "GetDetailPermision?groupID=" + groupID+"&roleID="+roleID);
            response.EnsureSuccessStatusCode();
            PermisionModel per = response.Content.ReadAsAsync<PermisionModel>().Result;
            return View(per);
        }
        [HttpPost]
        public ActionResult UpdatePermision(PermisionModel perUpdate)
        {
            HttpResponseMessage response = serviceObj.PutResponse(url + "UpdatePermision/", perUpdate);
            response.EnsureSuccessStatusCode();
            return RedirectToAction("Index");
        }
        public ActionResult DetailPermision(string groupID,int roleID)
        {
            HttpResponseMessage response = serviceObj.GetResponse(url + "GetDetailPermision?groupID=" + groupID+"&roleID="+roleID);
            response.EnsureSuccessStatusCode();
            PermisionModel per = response.Content.ReadAsAsync<PermisionModel>().Result;
            return View(per);
        }
        [HttpGet]
        public ActionResult CreatePermision() { 
            return View();
        }
        [HttpPost]
        public ActionResult CreatePermision(PermisionModel perInsert)
        {
            HttpResponseMessage response = serviceObj.PostResponse(url + "InsertPermision/", perInsert);
            response.EnsureSuccessStatusCode();
            return RedirectToAction("Index");
        }
        //public ActionResult DeleteGroupAdmin(string groupID,int roleID)
        //{
        //    HttpResponseMessage response = serviceObj.DeleteResponse(url + "DeletePermision/" + id);
        //    response.EnsureSuccessStatusCode();
        //    return RedirectToAction("Index");
        //}
    }
}