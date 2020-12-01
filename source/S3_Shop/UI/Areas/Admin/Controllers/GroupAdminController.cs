using Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;
using UI.Helpers;

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
            try
            {
                if (ModelState.IsValid)
                {
                    HttpResponseMessage response = serviceObj.PutResponse(url + "UpdateGroupAdmin", grUpdate);
                    response.EnsureSuccessStatusCode();
                    bool resultUpdate = response.Content.ReadAsAsync<bool>().Result;
                    if(resultUpdate)
                        return RedirectToAction("Index");
                    return ViewBag.ThongBao = "Chỉnh sửa thông tin thất bại";
                }
                return View("404");
            }
            catch (Exception)
            {
                return View("404");
                throw;
            }
            
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
            try
            {
                if (ModelState.IsValid)
                {
                    HttpResponseMessage response = serviceObj.PostResponse(url + "InsertGroupAdmin/", grInsert);
                    response.EnsureSuccessStatusCode();
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch (Exception)
            {
                return View("404");
                throw;
            }
        }
        public ActionResult DeleteGroupAdmin(string id)
        {
            try
            {
                HttpResponseMessage response = serviceObj.DeleteResponse(url + "DeleteGroupAdmin/" + id);
                response.EnsureSuccessStatusCode();
                bool resultDel = response.Content.ReadAsAsync<bool>().Result;
                if (resultDel)
                    return RedirectToAction("Index");
                else
                {
                    return View("ComingSoon");
                }
            }
            catch (Exception)
            {
                return View("404");
                throw;
            }
            
        }
    }
}