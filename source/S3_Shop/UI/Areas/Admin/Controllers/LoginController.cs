using Model;
using Model.Common;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using UI.Helpers;
using UI.Models;
using log4net;

namespace UI.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        private string url;
        private ServiceRepository serviceObj;
        private static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public LoginController()
        {
            serviceObj = new ServiceRepository();
            url = "https://localhost:44379/api/Employee_API/";
        }
        public ActionResult Index(string user)
        {
            log.Info("Load admin profile.");
            try
            {
                HttpResponseMessage response = serviceObj.GetResponse(url + "GetEmployeeByUsername?user=" + user);
                response.EnsureSuccessStatusCode();
                log.Info("Get information admin.");
                Model.EmployeeModel resultLogin = response.Content.ReadAsAsync<Model.EmployeeModel>().Result;
                return View(resultLogin);
            }
            catch (Exception)
            {
                log.Error("Cannot connect to admin profile.");
                return View("Login");
            }
            
        }
        [HttpPost]
        public ActionResult Index(EmployeeModel emUpdate)
        {
            log.Info("Prepare to update admin profile.");
            try
            {
                if(string.IsNullOrEmpty(emUpdate.FirstName))
                {
                    ModelState.AddModelError("","First name is unvalid.");
                    return View();
                }    
                if(string.IsNullOrEmpty(emUpdate.LastName))
                {
                    ModelState.AddModelError("","Last name is unvalid.");
                    return View();
                }    
                if (ModelState.IsValid)
                {
                    HttpResponseMessage response = serviceObj.PutResponse(url + "UpdateEmployee", emUpdate);
                    response.EnsureSuccessStatusCode();
                    log.Info("Get result update admin profile");
                    bool resultUpdate = response.Content.ReadAsAsync<bool>().Result;
                    
                    if (resultUpdate)
                        ViewBag.Result = "Thành công";
                    else
                        ViewBag.Result = "Thất bại";
                }
                else
                    ViewBag.Result = "Thiếu thông tin";
                log.Info("Result update admin profile: " + ViewBag.Result);
                return View();
            }
            catch (Exception)
            {
                log.Error("Cannnot connect to admin.");
                throw;
            }
            
        }
        public ActionResult Logout()
        {
            try
            {
                Session.Remove(Constants.ADMIN_SESSION);
                if (Response.Cookies["username"] != null)
                {
                    HttpCookie ckUser = new HttpCookie("username");
                    ckUser.Expires = DateTime.Now.AddHours(-48);
                    Response.Cookies.Add(ckUser);
                }
                if (Response.Cookies["password"] != null)
                {
                    HttpCookie ckPass = new HttpCookie("password");
                    ckPass.Expires = DateTime.Now.AddHours(-48);
                    Response.Cookies.Add(ckPass);
                }
                Constants.COUNT_LOGIN_FAIL_ADMIN = 0;
                return View("Login");
            }
            catch (Exception)
            {
                log.Error("Cannot logout admin.");
                return View("Login"); 
            }
        }
        public ActionResult Login()
        {
            try
            {
                LoginModel model = CheckAccount();
                if (model == null)
                    return View();
                else
                {
                    Session[Constants.ADMIN_SESSION] = model;
                    return RedirectToAction("Index", "Login", new { user = model.UserName });
                }
            }
            catch (Exception)
            {
                log.Error("Cannot connect to admin page.");
                throw;
            }
            
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            try
            {
                log.Info("Prepare to login admin");
                if (ModelState.IsValid)
                {
                    HttpResponseMessage response = serviceObj.GetResponse(url + "GetLoginResultByUsernamePassword?user=" + model.UserName + "&pass=" + model.Password);
                    response.EnsureSuccessStatusCode();
                    int resultLogin = response.Content.ReadAsAsync<int>().Result;
                    log.Info("Get result login admin.");
                    switch (resultLogin)
                    {
                        case 1:
                            {
                                HttpResponseMessage responseUser = serviceObj.GetResponse(url + "GetEmployeeByUsername?user=" + model.UserName);
                                responseUser.EnsureSuccessStatusCode();
                                EmployeeModel employLogin = responseUser.Content.ReadAsAsync<EmployeeModel>().Result;
                                log.Info("Save information admin on cookies.");
                                var adSession = new LoginModel();
                                adSession.UserName = employLogin.EmployName;
                                adSession.Password = employLogin.Pass;
                                adSession.GroupID = employLogin.GroupID;

                                //Lấy list danh sách phân quyền
                                HttpResponseMessage responsePermision = serviceObj.GetResponse(url + "GetPermisionByUsername?name=" + model.UserName);
                                responsePermision.EnsureSuccessStatusCode();
                                List<int> listPermision = responsePermision.Content.ReadAsAsync<List<int>>().Result;
                                Session.Add(Constants.SESSION_CREDENTIALS, listPermision);
                                log.Info("Get list permision of admin.");

                                //Xử lý remember me
                                Session.Add(Constants.ADMIN_SESSION, adSession);
                                if (model.RememberMe)
                                {
                                    HttpCookie ckUser = new HttpCookie("username");
                                    ckUser.Expires = DateTime.Now.AddHours(48);
                                    ckUser.Value = model.UserName;
                                    Response.Cookies.Add(ckUser);

                                    HttpCookie ckPass = new HttpCookie("password");
                                    ckPass.Expires = DateTime.Now.AddHours(48);
                                    ckPass.Value = model.Password;
                                    Response.Cookies.Add(ckPass);
                                }
                                Constants.COUNT_LOGIN_FAIL_ADMIN = 0;
                                return RedirectToAction("Index", "Login", new { user = adSession.UserName, pass = adSession.Password });
                            }
                        case 0:
                            ModelState.AddModelError("", "Tài khoản không tồn tại.");
                            log.Info("Tài khoản không tồn tại.");
                            break;
                        case -1:
                            ModelState.AddModelError("", "Tài khoản đang bị khoá.");
                            log.Info("Tài khoản đang bị khoá.");
                            break;
                        case -2:
                            ModelState.AddModelError("", "Mật khẩu không đúng.");
                            log.Info("Mật khẩu không đúng.");
                            break;
                        case -3:
                            ModelState.AddModelError("", "Đăng nhập sai quá 3 lần. Tài khoản bạn đã bị khóa.");
                            log.Info("Đăng nhập sai quá 3 lần. Tài khoản bạn đã bị khóa.");
                            break;
                        default:
                            ModelState.AddModelError("", "Đăng nhập thất bại.");
                            log.Info("Đăng nhập thất bại.");
                            break;
                    }
                }
                log.Info("Response result to admin");
                return this.View();
            }
            catch (Exception)
            {
                log.Error("Cannot login admin.");
                throw;
            }
            
        }
        public LoginModel CheckAccount()
        {
            LoginModel result=null;
            string username = string.Empty;
            string password = string.Empty;
            if (Request.Cookies["username"] != null)
                username = Request.Cookies["username"].Value;
            if (Request.Cookies["password"] != null)
                password = Request.Cookies["password"].Value;
            if (!string.IsNullOrEmpty(username) & !string.IsNullOrEmpty(password))
                result = new LoginModel { UserName = username, Password = password};
            return result;
        }
    }
}