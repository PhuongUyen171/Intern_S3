
using Facebook;
using Model;
using Model.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using UI.Helpers;
using UI.Models;

namespace UI.Controllers
{
    public class UserController : Controller
    {
        private string url;
        private ServiceRepository serviceObj;
        //private readonly ILogger<UserController> logger;
        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }
        public UserController()
        {
            serviceObj = new ServiceRepository();
            url = "https://localhost:44379/api/User_API/";
        }

        #region chức năng đăng nhập user
        public ActionResult Login()
        {
            UserLogin model = CheckAccount();
            if (model == null)
                return View();
            else
            {
                //Save token API
                Session[Constants.USER_SESSION] = model;
                string tokenUser = GetToken(model.UserName);
                HttpCookie cookie = new HttpCookie(Constants.TOKEN_NUMBER, tokenUser);
                Response.Cookies.Add(cookie);
                return RedirectToAction("ProfileUser", "User", new { usr = model.UserName });
            }
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLogin model)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = serviceObj.GetResponse(url + "GetLoginResultByUsernamePassword?user=" + model.UserName + "&pass=" + model.Password);
                response.EnsureSuccessStatusCode();
                int resultLogin = response.Content.ReadAsAsync<int>().Result;
                switch (resultLogin)
                {
                    case 1:
                        {
                            HttpResponseMessage responseUser = serviceObj.GetResponse(url + "GetCustomerByUsername?user=" + model.UserName);
                            responseUser.EnsureSuccessStatusCode();
                            CustomerModel customLogin = responseUser.Content.ReadAsAsync<CustomerModel>().Result;

                            UserLogin u = new UserLogin {
                                UserID = customLogin.CustomID,
                                UserName=customLogin.Username,
                                FullName=customLogin.CustomName,
                                Password=customLogin.Pass
                            };
                            Session.Add(Constants.USER_SESSION, u);
                            
                            //Save token API
                            string tokenUser = GetToken(u.UserName);
                            HttpCookie cookie = new HttpCookie(Constants.TOKEN_NUMBER,tokenUser);
                            HttpContext.Response.Cookies.Remove(Constants.TOKEN_NUMBER);
                            Response.Cookies.Add(cookie);
                            cookie.Expires = DateTime.Now.AddDays(1);
                            cookie.Value = tokenUser;
                            HttpContext.Response.SetCookie(cookie);

                            //Save cookies
                            HttpCookie ckUserAccount = new HttpCookie("usernameCustomer");
                            ckUserAccount.Expires = DateTime.Now.AddHours(48);
                            ckUserAccount.Value = u.UserName;
                            Response.Cookies.Add(ckUserAccount);

                            HttpCookie ckIDAccount = new HttpCookie("idCustomer");
                            ckIDAccount.Expires = DateTime.Now.AddHours(48);
                            ckIDAccount.Value = u.UserID + "";
                            Response.Cookies.Add(ckIDAccount);

                            HttpCookie ckNameAccount = new HttpCookie("nameCustomer");
                            ckNameAccount.Expires = DateTime.Now.AddHours(48);
                            ckNameAccount.Value = u.FullName;
                            Response.Cookies.Add(ckNameAccount);
                            return RedirectToAction("ProfileUser", "User", new { usr = customLogin.Username });
                        }
                    case 0:
                        ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không tồn tại.");
                        break;
                    default:
                        ModelState.AddModelError("", "Đăng nhập thất bại.");
                        break;
                }
            }
            return this.View();
        }
        public ActionResult Logout()
        {
            try
            {
                Session.Remove(Constants.USER_SESSION);
                if (Response.Cookies["usernameCustomer"] != null)
                {
                    HttpCookie ckUserAccount = new HttpCookie("usernameCustomer");
                    ckUserAccount.Expires = DateTime.Now.AddHours(-48);
                    Response.Cookies.Add(ckUserAccount);
                }
                if (Response.Cookies["idCustomer"] != null)
                {
                    HttpCookie ckIDAccount = new HttpCookie("idCustomer");
                    ckIDAccount.Expires = DateTime.Now.AddHours(-48);
                    Response.Cookies.Add(ckIDAccount);
                }
                if (Response.Cookies["nameCustomer"] != null)
                {
                    HttpCookie cknameAccount = new HttpCookie("nameCustomer");
                    cknameAccount.Expires = DateTime.Now.AddHours(-48);
                    Response.Cookies.Add(cknameAccount);
                }
                if (Response.Cookies[Constants.TOKEN_NUMBER] != null)
                {
                    HttpCookie cookie = new HttpCookie(Constants.TOKEN_NUMBER);
                    cookie.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(cookie);
                }
                Constants.COUNT_LOGIN_FAIL_USER = 0;
                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                return View("Login");
            }
        }

        #endregion

        #region chức năng quên mật khẩu
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgotPassword(ForgotPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                string resetCode = Guid.NewGuid().ToString();
                var verifyUrl = "/User/ResetPassword?id=" + resetCode + "&mail=" + model.Email;
                var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);
                HttpResponseMessage response = serviceObj.GetResponse(url + "GetCustomerByEmail?mail=" + model.Email);
                response.EnsureSuccessStatusCode();
                CustomerModel result = response.Content.ReadAsAsync<CustomerModel>().Result;
                if (result != null)
                {
                    ResetPasswordModel resetModel = new ResetPasswordModel();
                    resetModel.ResetCode = resetCode;
                    resetModel.Id = result.CustomID;
                    //This line I have added here to avoid confirm password not match issue , as we had added a confirm password property
                    var subject = "Password Reset Request";
                    var body = "Hi " + result.CustomName + ", <br/> You recently requested to reset your password for your account. Click the link below to reset it. " +
                            " <br/><br/><a href='" + link + "'>" + link + "</a> <br/><br/>" +
                            "If you did not request a password reset, please ignore this email or reply to let us know.<br/><br/> Thank you";
                    SendEmail(result.Email, body, subject);
                    ModelState.AddModelError("", "Mã code đã được gửi vào email của bạn");
                }
                else
                    ModelState.AddModelError("", "Địa chỉ email không tồn tại.");
                return View();
            }
            return View();
        }
        private void SendEmail(string emailAddress, string body, string subject)
        {
            using (MailMessage mm = new MailMessage("mlem1701@gmail.com", emailAddress))
            {
                mm.Subject = subject;
                mm.Body = body;
                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential("mlem1701@gmail.com", "ntpu1234");
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);
            }
        }
        public CustomerModel GetCustomerByEmail(string mail)
        {
            HttpResponseMessage response = serviceObj.GetResponse(url + "GetCustomerByEmail?mail=" + mail);
            response.EnsureSuccessStatusCode();
            CustomerModel result = response.Content.ReadAsAsync<CustomerModel>().Result;
            return result;
        }
        public ActionResult ResetPassword(string id, string mail)
        {
            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrEmpty(mail))
            {
                return View("~/Views/Shared/404.cshtml");
            }
            CustomerModel result = GetCustomerByEmail(mail);
            ResetPasswordModel mode = new ResetPasswordModel();
            mode.Id = result.CustomID;
            mode.Mail = mail;
            mode.ResetCode = id;
            return View(mode);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordModel model)
        {
            CustomerModel resultReset = GetCustomerByEmail(model.Mail);
            resultReset.Pass = model.NewPassword;
            HttpResponseMessage responseUpdate = serviceObj.PutResponse(url + "UpdateCustomer", resultReset);
            responseUpdate.EnsureSuccessStatusCode();
            bool result = responseUpdate.Content.ReadAsAsync<bool>().Result;
            if (result)
                return RedirectToAction("Login");
            ViewBag.Warning = "Có lỗi xảy ra trong quá trình đặt lại mật khẩu.";
            return this.View();
        }
        #endregion

        #region chức năng đăng kí
        public ActionResult Signin()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Signin([Bind(Include = "UserName,Email,Address,Password,ConfirmPassword,Name,Phone")] RegisterModel model, string AuthenticationCode)
        {
            var authenticationEmail = (AuthenticationEmail)Session[Constants.AUTHENTICATIONEMAIL_SESSION];
            if (ModelState.IsValid & authenticationEmail != null)
            {
                if (model.Email == authenticationEmail.Email & authenticationEmail.AuthenticationCode == AuthenticationCode)
                {
                    //check email đã đc dùng
                    var mail = GetCustomerByEmail(model.Email);
                    if (mail != null)
                    {
                        ModelState.AddModelError("","Email này đã được sử dụng.");
                        return View(model);
                    }

                    CustomerModel c = new CustomerModel
                    {
                        Username = model.UserName,
                        CustomName = model.Name,
                        Pass = model.Password,
                        Phone = model.Phone,
                        Location = model.Address,
                        Email = model.Email
                    };

                    HttpResponseMessage response = serviceObj.PostResponse(url + "InsertCustomer/", c);
                    response.EnsureSuccessStatusCode();

                    UserLogin u = new UserLogin
                    {
                        FullName = c.CustomName,
                        Password = c.Pass,
                        UserName = c.Username
                    };
                    //var userSession = new UserLogin();
                    //userSession.UserName = model.Name;
                    //userSession.Email = model.UserName;
                    Session[Constants.USER_SESSION] = null;
                    Session[Constants.USER_SESSION] = u;

                    return RedirectToAction("Login", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Mã xác thực không hợp lệ");
                    return View(model);
                }
            }
            return View(model);
        }
        #endregion

        #region chức năng đăng nhập bằng FB, GG
        public ActionResult LoginFacebook()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                response_type = "code",
                scope = "email",
            });

            return Redirect(loginUrl.AbsoluteUri);
        }
        public ActionResult FacebookCallback(string code)
        {
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                code = code
            });


            var accessToken = result.access_token;
            if (!string.IsNullOrEmpty(accessToken))
            {
                fb.AccessToken = accessToken;
                // Get the user's information, like email, first name, middle name etc
                dynamic me = fb.Get("me?fields=first_name,middle_name,last_name,id,email");
                string email = me.email;
                string userName = me.email;
                string firstname = me.first_name;
                string middlename = me.middle_name;
                string lastname = me.last_name;

                var user = new CustomerModel();
                user.Email = email;
                user.Username = email;
                user.Statu = true;
                user.CustomName = firstname + " " + middlename + " " + lastname;
                //user.CreatedDate = DateTime.Now;


                var resultInsert = InsertByFacebook(user);
                if (resultInsert > 0)
                {
                    UserLogin u = new UserLogin {
                        UserName=email,
                        UserID=resultInsert,
                        FullName=user.CustomName
                    };
                    Session.Add(Constants.USER_SESSION, u);

                    //Save token API
                    string token = GetToken(u.UserName);
                    HttpCookie cookie = new HttpCookie(Constants.TOKEN_NUMBER);
                    HttpContext.Response.Cookies.Remove(Constants.TOKEN_NUMBER);
                    cookie.Expires = DateTime.Now.AddDays(1);
                    cookie.Value = token;
                    HttpContext.Response.SetCookie(cookie);

                    //if (model.RememberMe)
                    //{
                    HttpCookie ckUserAccount = new HttpCookie("usernameCustomer");
                    ckUserAccount.Expires = DateTime.Now.AddHours(48);
                    ckUserAccount.Value = email;
                    Response.Cookies.Add(ckUserAccount);

                    HttpCookie ckIDAccount = new HttpCookie("idCustomer");
                    ckIDAccount.Expires = DateTime.Now.AddHours(48);
                    ckIDAccount.Value = resultInsert.ToString();
                    Response.Cookies.Add(ckIDAccount);

                    HttpCookie ckNameAccount = new HttpCookie("nameCustomer");
                    ckNameAccount.Expires = DateTime.Now.AddHours(48);
                    ckNameAccount.Value = u.FullName;
                    Response.Cookies.Add(ckNameAccount);
                }
            }
            return Redirect("/");
        }
        [HttpPost]
        public JsonResult LoginGoogle(string googleACModel)
        {
            ViewBag.GoogleSignIn = ConfigurationManager.AppSettings["GgAppId"].ToString();
            var accountSocialsList = new JavaScriptSerializer().Deserialize<List<AccountSocial>>(googleACModel);
            var accountSocials = accountSocialsList.FirstOrDefault();
            var memberAccount = new CustomerModel();
            memberAccount.Email = accountSocials.Email;
            memberAccount.Username = accountSocials.Email;
            memberAccount.CustomName = accountSocials.FullName;
            var resultInsert = InsertByGoogle(memberAccount);

            UserLogin u = new UserLogin
            {
                UserID = resultInsert,
                FullName = accountSocials.FullName,
                UserName = accountSocials.Email,
                Password = ""
            };
            //Save token API
            string token = GetToken(u.UserName);
            HttpCookie cookie = new HttpCookie(Constants.TOKEN_NUMBER);
            HttpContext.Response.Cookies.Remove(Constants.TOKEN_NUMBER);
            cookie.Expires = DateTime.Now.AddDays(1);
            cookie.Value = token;
            HttpContext.Response.SetCookie(cookie);

            //Save session
            Session.Remove(Constants.USER_SESSION);
            Session.Add(Constants.USER_SESSION, u);

            //Save cookies
            HttpCookie ckUserAccount = new HttpCookie("usernameCustomer");
            ckUserAccount.Expires = DateTime.Now.AddHours(48);
            ckUserAccount.Value = u.UserName;
            Response.Cookies.Add(ckUserAccount);

            HttpCookie ckIDAccount = new HttpCookie("idCustomer");
            ckIDAccount.Expires = DateTime.Now.AddHours(48);
            ckIDAccount.Value = u.UserID + "";
            Response.Cookies.Add(ckIDAccount);

            HttpCookie ckNameAccount = new HttpCookie("nameCustomer");
            ckNameAccount.Expires = DateTime.Now.AddHours(48);
            ckNameAccount.Value = u.FullName;
            Response.Cookies.Add(ckNameAccount);

            return Json(new { status = true });
        }
        public int InsertByFacebook(CustomerModel customer)
        {
            HttpResponseMessage response = serviceObj.PostResponse(url + "InsertForFacebook/", customer);
            response.EnsureSuccessStatusCode();
            int resultInsert = response.Content.ReadAsAsync<int>().Result;
            return resultInsert;
        }
        public int InsertByGoogle(CustomerModel customer)
        {
            HttpResponseMessage response = serviceObj.PostResponse(url + "InsertForGoogle/", customer);
            response.EnsureSuccessStatusCode();
            int resultInsert = response.Content.ReadAsAsync<int>().Result;
            return resultInsert;
        }
        #endregion

        #region cập nhật thông tin
        [HttpGet]
        public ActionResult ProfileUser(string usr)
        {
            CustomerModel cus = GetCustomerByUsername(usr);
            return View(cus);
        }
        [HttpPost]
        public ActionResult ProfileUser(CustomerModel model)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = serviceObj.PutResponse(url + "UpdateCustomer", model);
                //Change token
                HttpCookie cookie = HttpContext.Request.Cookies.Get(Constants.TOKEN_NUMBER);
                string token = cookie.Value.ToString();

                response.EnsureSuccessStatusCode();
                bool resultUpdate = response.Content.ReadAsAsync<bool>().Result;
                if (resultUpdate)
                    ViewBag.Result = "Cập nhật thông tin thành công.";
                else
                    ViewBag.Result = "Có lỗi xảy ra trong quá trình cập nhật.";
            }
            else
                ModelState.AddModelError("","Thiếu thông tin.");
            return View(model);
        }
        #endregion

        #region get information customer
        [HttpGet]
        public CustomerModel GetCustomerByUsername(string user)
        {
            HttpResponseMessage response = serviceObj.GetResponse(url + "GetCustomerByUsername?user=" + user);
            response.EnsureSuccessStatusCode();
            CustomerModel result = response.Content.ReadAsAsync<CustomerModel>().Result;
            return result;
        }
        public CustomerModel GetCustomerByToken(string token)
        {
            HttpResponseMessage response = serviceObj.GetResponse(url + "GetCustomerByToken?token=" + token);
            response.EnsureSuccessStatusCode();
            CustomerModel result = response.Content.ReadAsAsync<CustomerModel>().Result;
            return result;
        }
        public string GetToken(string username)
        {
            HttpResponseMessage response = serviceObj.GetResponse(url + "GetToken?username=" + username);
            response.EnsureSuccessStatusCode();
            return response.Content.ReadAsAsync<string>().Result;
        }
        public JsonResult GetAuthenticationInEmail(string Email)
        {
            var findThisEmail = GetCustomerByEmail(Email);
            if (findThisEmail == null)
            {
                Session[Constants.AUTHENTICATIONEMAIL_SESSION] = null;
                int authCode = new Random().Next(1000, 9999);
                AuthenticationEmail authenticationEmail = new AuthenticationEmail();
                authenticationEmail.Email = Email;
                authenticationEmail.AuthenticationCode = authCode.ToString();
                Session[Constants.AUTHENTICATIONEMAIL_SESSION] = authenticationEmail;

                MailHelper.SendMailAuthentication(Email, "Mã xác thực từ Moji Store", authCode.ToString());

                return Json(new { status = true });
            }
            else
                return Json(new { status = false });
        }
        #endregion

        public ActionResult UserPartial()
        {
            return PartialView();
        }
        public UserLogin CheckAccount()
        {
            UserLogin result = null;
            string username = string.Empty;
            string id = string.Empty;
            string fullname = string.Empty;
            if (Request.Cookies["usernameCustomer"] != null)
                username = Request.Cookies["usernameCustomer"].Value;
            if (Request.Cookies["idCustomer"] != null)
                id = Request.Cookies["idCustomer"].Value;
            if (Request.Cookies["nameCustomer"] != null)
                fullname = Request.Cookies["nameCustomer"].Value;
            if (!string.IsNullOrEmpty(username) & !string.IsNullOrEmpty(id) &!string.IsNullOrEmpty(fullname))
                result = new UserLogin { UserID = int.Parse(id), UserName = username,FullName=fullname };
            return result;
        }
        //public async Task<ActionResult> GetToken()
        //{
        //    var token = string.Empty;
        //    using(var client =new HttpClient())
        //    {
        //        client.DefaultRequestHeaders.Clear();
        //        client.BaseAddress = new Uri(url);
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        var response = await client.GetAsync(url+"ValidLogin?user=admin&pass=admin");
        //        if(response.IsSuccessStatusCode)
        //        {
        //            var result = response.Content.ReadAsStringAsync().Result;
        //            token = JsonConvert.DeserializeObject<string>(result);
        //            Session[Constants.TOKEN_NUMBER] = token;
        //            Session["TEST"] = "admin";
        //        }    
        //    }
        //    return Content(token);
        //}
        //[HttpGet]
        //[CustomAuthenticationFilter]
        //public async Task<ActionResult> GetEmployee()
        //{
        //    var result = string.Empty;
        //    using (var client = new HttpClient())
        //    {
        //        client.DefaultRequestHeaders.Clear();
        //        client.BaseAddress = new Uri(url);
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Session[Constants.TOKEN_NUMBER].ToString()+":"+Session["TEST"].ToString());
        //        var response = await client.GetAsync(url+"ValidLogin?user=admin&pass=admin");
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var token = response.Content.ReadAsStringAsync().Result;
        //            result = JsonConvert.DeserializeObject<string>(token);
        //            //Session[Constants.TOKEN_NUMBER] = token;
        //        }
        //    }
        //    return Content(result);
        //}
    }
}