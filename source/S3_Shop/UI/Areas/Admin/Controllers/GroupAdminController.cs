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
    public class GroupAdminController : Controller
    {
        string url;
        ServiceRepository serviceObj;
        public GroupAdminController()
        {
            serviceObj = new ServiceRepository();
            url = "https://localhost:44379/api/GroupAdmin_API/";
        }
        public ActionResult Index()
        {
            try
            {
                HttpResponseMessage response = serviceObj.GetResponse(url + "GetAllGroupAdmins");
                response.EnsureSuccessStatusCode();
                List<GroupAdminModel> list = response.Content.ReadAsAsync<List<GroupAdminModel>>().Result;
                return View(list);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public ActionResult EditGroupAdmin(string id)
        {
            HttpResponseMessage response = serviceObj.GetResponse(url + "GetDetailGroupAdmin/" + id);
            response.EnsureSuccessStatusCode();
            GroupAdminModel gr = response.Content.ReadAsAsync<GroupAdminModel>().Result;
            return View(gr);
        }
        [HttpPost]
        public ActionResult UpdateGroupAdmin(GroupAdminModel grUpdate)
        {
            HttpResponseMessage response = serviceObj.PutResponse(url + "UpdateGroupAdmin/", grUpdate);
            response.EnsureSuccessStatusCode();
            return RedirectToAction("Index");
        }
        public ActionResult DetailGroupAdmin(string id)
        {
            HttpResponseMessage response = serviceObj.GetResponse(url + "GetDetailGroupAdmin/" + id);
            response.EnsureSuccessStatusCode();
            GroupAdminModel gr = response.Content.ReadAsAsync<GroupAdminModel>().Result;
            return View(gr);
        }
        [HttpGet]
        public ActionResult CreateGroupAdmin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateGroupAdmin(GroupAdminModel grInsert)
        {
            HttpResponseMessage response = serviceObj.PostResponse(url + "InsertGroupAdmin/", grInsert);
            response.EnsureSuccessStatusCode();
            return RedirectToAction("Index");
        }
        public ActionResult DeleteGroupAdmin(string id)
        {
            HttpResponseMessage response = serviceObj.DeleteResponse(url + "DeleteGroupAdmin/" + id);
            response.EnsureSuccessStatusCode();
            return RedirectToAction("Index");
        }
    }
}