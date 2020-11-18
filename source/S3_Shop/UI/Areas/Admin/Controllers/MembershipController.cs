using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Model;
using Model.Common;

namespace UI.Areas.Admin.Controllers
{
    public class MembershipController : Controller
    {
        string url;
        ServiceRepository serviceObj;
        public MembershipController()
        {
            serviceObj = new ServiceRepository();
            url = "https://localhost:44379/api/Membership_API/";
        }
        [HasPermision(RoleID = "3")]
        public ActionResult Index()
        {
            try
            {
                HttpResponseMessage response = serviceObj.GetResponse(url + "GetAllMemberships");
                response.EnsureSuccessStatusCode();
                List<MembershipModel> list = response.Content.ReadAsAsync<List<MembershipModel>>().Result;
                return View(list);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public ActionResult EditMembership(string id)
        {
            HttpResponseMessage response = serviceObj.GetResponse(url + "GetDetailMembership/" + id);
            response.EnsureSuccessStatusCode();
            MembershipModel mem = response.Content.ReadAsAsync<MembershipModel>().Result;
            return View(mem);
        }
        [HttpPost]
        public ActionResult UpdateMembership(MembershipModel memUpdate)
        {
            HttpResponseMessage response = serviceObj.PutResponse(url + "UpdateMembership/", memUpdate);
            response.EnsureSuccessStatusCode();
            return RedirectToAction("Index");
        }
        public ActionResult DetailMembership(string id)
        {
            HttpResponseMessage response = serviceObj.GetResponse(url + "GetDetailMembership/" + id);
            response.EnsureSuccessStatusCode();
            MembershipModel mem = response.Content.ReadAsAsync<MembershipModel>().Result;
            return View(mem);
        }
        [HttpGet]
        public ActionResult CreateMembership()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateMembership(MembershipModel memInsert)
        {
            HttpResponseMessage response = serviceObj.PostResponse(url + "InsertMembership/", memInsert);
            response.EnsureSuccessStatusCode();
            return RedirectToAction("Index");
        }
        public ActionResult DeleteMembership(string id)
        {
            HttpResponseMessage response = serviceObj.DeleteResponse(url + "DeleteMembership/" + id);
            response.EnsureSuccessStatusCode();
            return RedirectToAction("Index");
        }
    }
}