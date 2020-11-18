using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Model;

namespace UI.Areas.Admin.Controllers
{
    public class RoleController : Controller
    {
        private ServiceRepository serviceObj;
        private string url;
        public RoleController()
        {
            url = "https://localhost:44379/api/Role_API/";
            serviceObj = new ServiceRepository();
        }
        public ActionResult Index()
        {
            try
            {
                HttpResponseMessage response = serviceObj.GetResponse(url + "GetAllRoles");
                response.EnsureSuccessStatusCode();
                List<RoleModel> list = response.Content.ReadAsAsync<List<RoleModel>>().Result;
                return View(list);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public ActionResult EditRole(int id)
        {
            HttpResponseMessage response = serviceObj.GetResponse(url + "GetDetailRole/" + id);
            response.EnsureSuccessStatusCode();
            RoleModel role = response.Content.ReadAsAsync<RoleModel>().Result;
            return View(role);
        }
        [HttpPost]
        //Không tác động thêm xóa sỬA
        public ActionResult UpdateRole(RoleModel role)
        {
            HttpResponseMessage response = serviceObj.PutResponse(url + "UpdateRole/", role);
            response.EnsureSuccessStatusCode();
            return RedirectToAction("Index");
        }
        public ActionResult DetailRole(int id)
        {
            HttpResponseMessage response = serviceObj.GetResponse(url + "GetDetailRole/" + id);
            response.EnsureSuccessStatusCode();
            RoleModel role = response.Content.ReadAsAsync<RoleModel>().Result;
            return View(role);
        }
        [HttpGet]
        public ActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateRole(RoleModel role)
        {
            HttpResponseMessage response = serviceObj.PostResponse(url + "InsertRole/", role);
            response.EnsureSuccessStatusCode();
            return RedirectToAction("Index");
        }
        public ActionResult DeleteRole(int id)
        {
            HttpResponseMessage response = serviceObj.DeleteResponse(url + "DeleteRole/" + id);
            response.EnsureSuccessStatusCode();
            return RedirectToAction("Index");
        }
    }
}